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

    //private IEnumerator InitializeCamera()
    //{
    //    // Wait to avoid potential camera crashes
    //    yield return new WaitForSeconds(1.25f);
    //}

    void Update()
    {
        IBarcodeReader reader = new BarcodeReader();

        var result = reader.Decode(webCamTexture.GetPixels32(), webCamTexture.width, webCamTexture.height);
        if (result != null)
        {
            text.text = result.Text;
            webCamTexture.Stop();
        }

        //if (webCamTexture.width > 100)
        //{
            
        //}
    }
}
