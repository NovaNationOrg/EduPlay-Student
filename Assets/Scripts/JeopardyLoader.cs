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
   
}
