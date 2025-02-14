using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JeopardyLoader
{
    public Dictionary<string, Dictionary<string , string>> themeQuestions;

    public void LoadThemes(string[] qrData)
    {
        themeQuestions = new Dictionary<string, Dictionary<string, string>>();
        for (int i = 2; i < qrData.Length; i += 11)
        {
            themeQuestions[qrData[i]] = new ();
        }
    }
    public void loadQuestions(string[] qrQuestions)
    {
        Dictionary<string, string> questionList = new();
        for (int i = 2; i < qrQuestions.Length; i++)
        {

            if ((i - 2) % 11 == 0)
            {
                questionList = themeQuestions[qrQuestions[i]];

            }
            else
            {
                questionList[qrQuestions[i]] = qrQuestions[i+1];
                i++;

            }
            
        }
    }

    public Dictionary<string, Dictionary<string, string>> GetGameAttributes()
    {
        return themeQuestions;
    }
}
