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

    public void UpdateScore(int currentScore, int bestScore)
    {
        _currentScore.SetText($"{currentScore}");
        _bestScore.SetText($"best: {bestScore}");
    }
}
