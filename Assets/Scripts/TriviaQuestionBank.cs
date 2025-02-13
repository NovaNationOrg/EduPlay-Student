using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TriviaQuestionBank
{
    public static Dictionary<string, ArrayList> triviaData;

    public static ArrayList GetCategories()
    {
        ArrayList categories = new();
        foreach(string category in triviaData.Keys)
        {
            categories.Add(category);
        }
        return categories;
    }

    public static ArrayList GetQuestions(string category)
    {
        return triviaData[category];
    }
}
