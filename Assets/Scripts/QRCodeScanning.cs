using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRCodeScanning : MonoBehaviour
{
    public RawImage image;
    public TextMeshProUGUI infoText;

    public List<WebCamDevice> devices = new();
    public WebCamTexture mainWebCamTexture;

    public bool onLaptop;
    public Vector3 phoneRes = new Vector3(1024, 720, 30);

    public float scanInterval = 0.5f;
    public string deviceName;

    private bool isScanning = false;
    private readonly string[] commands = { "_jp_", "_pr_", "_add_", "_mul_" };

    public Button startButton;
    public Button returnButton;

    void Start() { StartCoroutine(RequestCameraPermission()); }

    IEnumerator RequestCameraPermission()
    {
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // Let the app wait each frame until the user accepts the permission
        while (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            yield return null;

        devices = WebCamTexture.devices.ToList<WebCamDevice>();
        returnButton.onClick.AddListener(ReturnScene);
        if (startButton != null)
            startButton.onClick.AddListener(TransferScenes);
        else
            Debug.LogError("Start Button was not set!");

        if (devices.Count > 0)
        {
            onLaptop = true;

            for (int i = 0; i < devices.Count; i++)
            {
                if (!devices[i].isFrontFacing) // We just want the rear cam
                {
                    onLaptop = false;

                    deviceName = devices[i].name;
                    SetupWebCamTexture(devices[i].name);
                    break; // We only want the first rear Web Cam (in cases where the user has multiple)
                }
            }

            // To accommodate those individuals using this app on a PC
            if (onLaptop)
            {
                infoText.gameObject.SetActive(true);
                infoText.text = "This is meant for phones, but you chillin'";

                deviceName = devices[0].name;
                SetupWebCamTexture(devices[0].name);
            }
        }
        else
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "No available camera found on device!";

            image.gameObject.SetActive(false);
        }
    }

    private void SetupWebCamTexture(string newDeviceName)
    {
        mainWebCamTexture = new WebCamTexture(newDeviceName);

        if (mainWebCamTexture != null)
        {
            image.texture = mainWebCamTexture;
            image.material.mainTexture = mainWebCamTexture;

            StartCoroutine(SetUpCamera());
        }
    }

    private IEnumerator SetUpCamera()
    {
        // Let the camera initialize to prevent crashes
        yield return new WaitForSeconds(0.5f);

        mainWebCamTexture.Play();

        UpdateCameraDisplay();

        StartCoroutine(ScanBarcode());
    }

    private IEnumerator ScanBarcode()
    {
        while (mainWebCamTexture != null)
        {
            if (!isScanning)
            {
                isScanning = true;

                if (mainWebCamTexture.width > 100)
                {
                    IBarcodeReader reader = new BarcodeReader();

                    var result = reader.Decode(mainWebCamTexture.GetPixels32(), image.texture.width, image.texture.height);

                    if (result != null)
                        ReadData(result.Text);
                }

                yield return new WaitForSeconds(scanInterval);
                isScanning = false;
            }
        }
    }

    private void UpdateCameraDisplay()
    {
        // Get camera rotation and adjust UI accordingly
        int rotationAngle = -mainWebCamTexture.videoRotationAngle;

        // If someone manages to get the device upside down...
        if (mainWebCamTexture.videoVerticallyMirrored)
            rotationAngle += 180;

        image.rectTransform.localEulerAngles = new Vector3(0, 0, rotationAngle);
    }

    private void DisableReader() { mainWebCamTexture.Stop(); }

    private void ReadData(string qrData)
    {
        infoText.text = qrData;

        if (IsValidCommand(qrData))
        {
            infoText.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);

            QRCodeProcessing.SetContent(qrData);
        }
    }

    private void TransferScenes() { SceneSwitcher.SwitchGameScene("EduPlayJeopardy"); }
    private void ReturnScene() { SceneSwitcher.SwitchGameScene("LandingPage"); }

    private bool IsValidCommand(String command)
    {
        String code;
        if (command.IndexOf("\n") != -1)
            code = command.Substring(0, command.IndexOf("\n"));
        else
            code = command.Substring(0, command.Length);

        foreach (string comm in commands)
        {
            if (code == comm)
                return true;
        }

        return false;
    }
}