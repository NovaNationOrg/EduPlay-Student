using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using ZXing;

public class QRCodeProcessing : MonoBehaviour
{

    public RawImage image;
    public TextMeshProUGUI text;

    public WebCamTexture webCamTexture;

    void Start()
    {
        webCamTexture = new WebCamTexture();

        image.texture = webCamTexture;
        image.material.mainTexture = webCamTexture;

        webCamTexture.Play();
    }

    void Update()
    {
        if (webCamTexture.width > 100) // This is just a way to determine if the camera is initializaed
        {
            IBarcodeReader reader = new BarcodeReader();

            UpdateCameraDisplay();
            AdjustAspectRatio();

            var result = reader.Decode(webCamTexture.GetPixels32(), image.texture.width, image.texture.height);
            if (result != null)
            {
                text.text = result.Text;

                // Do important stuff here or something
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
}
