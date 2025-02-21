using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QRCodeProcessing : MonoBehaviour
{
    private static string qrContent;
    public Button returnButton;
    public TextMeshProUGUI displayText;

    private void Start() { displayText.text = ProcessString(qrContent); }

    public void SceneReturn() { SceneSwitcher.SwitchScanScene(); }
    public static void SetContent(string content) { qrContent = content; }

    private string ProcessString(string content)
    {
        string[] str = content.Split("\n");
        string dpText = "";
        if (str[0] == "_jp_")
        {
            if (LoadJeopardy(str))
            {
                dpText = "Select a Category";
                GameManager.SetupCategories();
            }
            else
                dpText = "Invalid Jeopardy Qr package";
        }
        else if (str[0] == "_pr_")
            dpText = str[1];
        else if (str[0] == "_add_")
            dpText = (Int32.Parse(str[1]) + Int32.Parse(str[2])).ToString();
        else if (str[0] == "_mul_")
            dpText = (Int32.Parse(str[1]) * Int32.Parse(str[2])).ToString();
        return dpText;
    }

    private bool LoadJeopardy(string[] str)
    {
        if(str.Length != 57)
            return false;
        
        JeopardyLoader jeopardyLoader = new JeopardyLoader();
        jeopardyLoader.LoadThemes(str);
        jeopardyLoader.LoadQuestions(str);
      
        TriviaQuestionBank.triviaData = jeopardyLoader.GetGameAttributes();

        return true;
    }
}
