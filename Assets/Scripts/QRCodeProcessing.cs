using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QRCodeProcessing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private static string qrContent;
    public Button returnButton;
    public TextMeshProUGUI displayText;
    public SceneSwitcher sceneSwitcher = new();

    // Update is called once per frame

 

    private void Update()
    {

        displayText.text = processString(qrContent);
    }

    public static void setContent(string content)
    {
        qrContent = content;
    }
 
   
    private string processString(string content)
    {
        
        string[] str = content.Split("\n");
        string dpText = "";
        if (str[0] == "_jp_")
            dpText = "Welcome to jeopardy";
        else if (str[0] == "_pr_")
            dpText = str[1];
        else if (str[0] == "_add_")
            dpText = (Int32.Parse(str[1]) + Int32.Parse(str[2])).ToString();
        else if (str[0] == "_mul_")
            dpText = (Int32.Parse(str[1]) * Int32.Parse(str[2])).ToString();
        return dpText;

    }



}
