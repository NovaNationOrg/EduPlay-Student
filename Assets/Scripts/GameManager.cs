using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static List<string> categories = new();
    private static List<string> questions = new();

    private static string selectedCategory;
    private static string selectedQuestion;

    public static void SetupCategories()
    {
        // Cast the ArrayList to List for easier manipulation
        categories = TriviaQuestionBank.GetCategories().Cast<string>().ToList();
    }

    public static void SetCategory(string category) { selectedCategory = category; }
    public static void SetQuestion(string question) { selectedQuestion = question; }

    public static string GetQuestion() { return selectedQuestion; }

    public static List<string> GetCategories() {  return categories; }
    public static List<string> GetQuestions()
    {
        questions = TriviaQuestionBank.GetQuestions(selectedCategory).Cast<string>().ToList();

        if (questions != null)
            return questions;

        return null;
    }
}
