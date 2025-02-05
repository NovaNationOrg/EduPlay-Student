using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRCodeScanning : MonoBehaviour
{

    public RawImage image;
    public TextMeshProUGUI text;
    public WebCamTexture webCamTexture;
    private readonly string[] commands = {"_jp_","_pr_","_add_","_mul_"};
    void Start()
    {
        webCamTexture = new WebCamTexture();
        image.texture = webCamTexture;
        image.material.mainTexture = webCamTexture;

        StartCoroutine(SetUpCamera());
    }

    private IEnumerator SetUpCamera()
    {
        // Let the camera initialize to prevent crashes
        yield return new WaitForSeconds(0.5f);

        webCamTexture.Play();

        UpdateCameraDisplay();
        AdjustAspectRatio();
    }

    void Update()
    {
        if (webCamTexture.width > 100) // This is just a way to determine if the camera is initializaed
        {
            IBarcodeReader reader = new BarcodeReader();

            var result = reader.Decode(webCamTexture.GetPixels32(), image.texture.width, image.texture.height);
            if (result != null)
            {
                ReadData(result.Text);    
            }
        }
    }

    private void UpdateCameraDisplay()
    {
        // Get camera rotation and adjust UI accordingly
        int rotationAngle = -webCamTexture.videoRotationAngle;

        // If someone manages to get the device upside down...
        if (webCamTexture.videoVerticallyMirrored)
            rotationAngle += 180; 

        image.rectTransform.localEulerAngles = new Vector3(0, 0, rotationAngle);
    }

    void AdjustAspectRatio()
    {
        float cameraAspect = (float)webCamTexture.width / webCamTexture.height;
        float screenAspect = (float)Screen.width / Screen.height;

        if (cameraAspect > screenAspect)
            image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width / cameraAspect);
        else
            image.rectTransform.sizeDelta = new Vector2(Screen.height * cameraAspect, Screen.height);
    }

    private void DisableReader()
    {
        webCamTexture.Stop();
    }

    private void ReadData(string qrData)
    {
        text.text = qrData;

        if (IsValidCommand(qrData))
        {
           QRCodeProcessing.SetContent(qrData);
           SceneSwitcher.SwitchScreen1();

        }
    }

    private bool IsValidCommand(String command)
    {
        String code;
        if (command.IndexOf("\n") != -1)
           code = command.Substring(0,command.IndexOf("\n"));
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
