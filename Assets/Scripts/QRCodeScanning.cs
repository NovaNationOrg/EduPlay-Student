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
    string[] commands = {"_jp_","_pr_","_add_","_mul_"};
    SceneSwitcher sceneSwitcher = new SceneSwitcher();
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
                readData(result.Text);    
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

    private void readData(string qrData)
    {
        text.text = qrData;

        if (isValidCommand(qrData))
        {
            QRCodeProcessing.setContent(qrData);
            sceneSwitcher.switchScreen1();

        }
    }

    private bool isValidCommand(String text)
    {
        String code;
        if (text.IndexOf("\n") != -1)
           code = text.Substring(0,text.IndexOf("\n"));
        else
            code = text.Substring(0, text.IndexOf("\0"));


        foreach (string comm in commands){
            if (code == comm)
                return true;
        }

        return false;

        
    }

   
}
