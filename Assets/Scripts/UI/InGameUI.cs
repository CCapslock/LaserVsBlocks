using UnityEngine;

public class InGameUI : BaseMenu
{
    [Header ("Panel of InGameUI")]
    [SerializeField] private GameObject _mainPanel;

    [Header ("Score")]
    [SerializeField] private TextUI _currentScore;
    [SerializeField] private TextUI _bestScore;

    [Header("Pause button")]
    [SerializeField] private ButtonUI _pauseButton;

    private UIController _uiController;

    private void Start()
    {
        _uiController = GetComponentInParent<UIController>();

        _pauseButton.GetControl.onClick.AddListener(delegate
        {
            _uiController.PauseGame();
        });
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _mainPanel.gameObject.SetActive(false);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        _mainPanel.gameObject.SetActive(true);
        IsShow = true;
    }

    public void UpdateScore(float currentScore, float bestScore)
    {
        _currentScore.SetText($"{Mathf.Floor(currentScore)}");
        _bestScore.SetText($"best: {Mathf.Floor(bestScore)}");
    }
}
