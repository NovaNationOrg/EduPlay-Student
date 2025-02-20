using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    private List<string> questions;

    public Button tabTemplate;
    public GameObject content;

    public Button returnButton;

    void Start()
    {
        questions = GameManager.GetQuestions();

        if (returnButton != null)
            returnButton.onClick.AddListener(ReturnToCategories);

        foreach (string question in questions)
        {
            Button newTab = Instantiate(tabTemplate, content.transform);
            newTab.onClick.AddListener(SetupQuestion);

            newTab.name = question;
            newTab.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = question;

            newTab.gameObject.SetActive(true);
        }
    }

    private void ReturnToCategories() { SceneSwitcher.SwitchGameScene("EduPlayJeopardy"); }

    private void TransferScenes(string sceneName) { SceneSwitcher.SwitchGameScene(sceneName); }

    private void SetupQuestion()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameManager.SetQuestion(button.name);

        TransferScenes("EduPlayJeopardyQuestion");
    }
}
