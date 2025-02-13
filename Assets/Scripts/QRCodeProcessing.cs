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

    // Update is called once per frame


    private void Start()
    {
        displayText.text = ProcessString(qrContent);
    }

   

    public void SceneReturn()
    {
        SceneSwitcher.SwitchScreen2();
    }
    public static void SetContent(string content)
    {
        qrContent = content;
    }
 
   
    private string ProcessString(string content)
    {
        
        string[] str = content.Split("\n");
        string dpText = "";
        if (str[0] == "_jp_")
        {
            loadJeopardy(str);
            dpText = "Welcome to jeopardy";

        }
        else if (str[0] == "_pr_")
            dpText = str[1];
        else if (str[0] == "_add_")
            dpText = (Int32.Parse(str[1]) + Int32.Parse(str[2])).ToString();
        else if (str[0] == "_mul_")
            dpText = (Int32.Parse(str[1]) * Int32.Parse(str[2])).ToString();
        return dpText;

    }

    private void loadJeopardy(string[] str)
    {
        JeopardyLoader jp = new JeopardyLoader();
        jp.LoadThemes(str);

        
    }

}
