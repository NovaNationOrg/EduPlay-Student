using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JeopardyLoader
{
    public Dictionary<string, ArrayList> themeQuestions;

    public void LoadThemes(string[] qrData)
    {
        themeQuestions = new Dictionary<string, ArrayList>();
        for (int i = 2; i < qrData.Length; i += 6)
        {
            themeQuestions[qrData[i]] = new ArrayList();
        }
    }
    public void loadQuestions(string[] qrQuestions)
    {
        ArrayList questionList = new();
        for (int i = 2; i < qrQuestions.Length; i++)
        {

            if ((i - 2) % 6 == 0)
            {
                questionList = themeQuestions[qrQuestions[i]];

            }
            else
            {
                questionList.Add(qrQuestions[i]);

            }
            
        }
    }

    public Dictionary<string, ArrayList> GetGameAttributes()
    {
        return themeQuestions;
    }
}
