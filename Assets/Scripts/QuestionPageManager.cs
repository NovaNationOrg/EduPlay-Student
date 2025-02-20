using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPageManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI questionText;

    public TextMeshProUGUI answerField;

    public Button submitButton;
    public Button returnButton;

    void Start()
    {
        questionText.text = GameManager.GetQuestion();

        if (returnButton != null)
            returnButton.onClick.AddListener(ReturnToQuestions);
    }

    private void ReturnToQuestions() { SceneSwitcher.SwitchGameScene("EduPlayJeopardyCategory"); }
}
