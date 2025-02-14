using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TriviaQuestionBank
{
    public static Dictionary<string, Dictionary<string, string>> triviaData;

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
        ArrayList questions = new();
        foreach (string question in triviaData[category].Keys)
        {
            questions.Add(question);
        }
        return questions;
    }

    public static string GetAnswer(string category,string question)
    {
        Dictionary<string, string> questions = triviaData[category];
        string answer = questions[question];

        return answer;
    }
}
