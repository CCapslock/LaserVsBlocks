using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : BaseMenu
{
    [Header("Panel of EndGame Menu")]
    [SerializeField] private GameObject _mainPanel;

    [Header("Text of EndGame Menu")]
    [SerializeField] private TextUI _headerEndGame;
    [SerializeField] private TextUI _currentScore;
    [SerializeField] private TextUI _bestScore;

    [Header("Buttons of EndGame Menu")]
    [SerializeField] private ButtonUI _buttonRestart;

    private UIController _uiController;

    private void Start()
    {
        _uiController = GetComponentInParent<UIController>();

        _buttonRestart.GetControl.onClick.AddListener(delegate
        {
            _uiController.RestartGame();
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
}
