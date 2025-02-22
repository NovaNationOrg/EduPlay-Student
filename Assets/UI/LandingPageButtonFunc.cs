using UnityEngine;
using UnityEngine.UIElements;

public class LandingPageButtonFunc: MonoBehaviour
{
    public VisualElement ui;

    public Button joinButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        joinButton = ui.Q<Button>("joinButton");
        joinButton.clicked += JoinButtonClicked;
    }

    private void JoinButtonClicked()
    {
        SceneSwitcher.SwitchScanScene();
    }
}
