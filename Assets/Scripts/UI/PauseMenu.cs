using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    [Header ("Panel of Pause Menu")]
    [SerializeField] private GameObject _mainPanel;

    [Header ("Text of Pause Menu")]
    [SerializeField] private TextUI _headerPause;

    [Header ("Buttons of Pause Menu")]
    [SerializeField] private ButtonUI _buttonResume;
    [SerializeField] private ButtonUI _buttonRestart;

    private UIController _uiController;

    private void Start()
    {
        _uiController = GetComponentInParent<UIController>();

        _buttonResume.GetControl.onClick.AddListener(delegate
        {
            _uiController.ResumeGame();
        });
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
