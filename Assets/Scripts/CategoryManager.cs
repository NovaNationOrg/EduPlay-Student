using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CategoryManager : MonoBehaviour
{
    private List<string> categories;

    public Button tabTemplate;
    public GameObject content;

    public Button returnButton;

    void Start()
    {
        categories = GameManager.GetCategories();

        if (returnButton != null)
            returnButton.onClick.AddListener(ReturnToLanding);

        foreach (string category in categories)
        {
            Button newTab = Instantiate(tabTemplate, content.transform);
            newTab.onClick.AddListener(SetupQuestions);

            newTab.name = category;
            newTab.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = category;

            newTab.gameObject.SetActive(true);
        }
    }

    // Temporarily will return to the scanner
    private void ReturnToLanding() { SceneSwitcher.SwitchGameScene("EduPlayStudent"); }

    private void TransferScenes(string sceneName) { SceneSwitcher.SwitchGameScene(sceneName); }

    private void SetupQuestions()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameManager.SetCategory(button.name);

        TransferScenes("EduPlayJeopardyCategory");
    }
}
