using UnityEngine;
using UnityEngine.UI;
public class MainMenu : BaseMenu
{
    [Header ("Panel of Main Menu")]
    [SerializeField] private GameObject _mainPanel;

    [Header ("Tap to start button")]
    [SerializeField] private ButtonUI _tapToStart;

    private Text _textTapToStart;
    private float _timer;

    private void Start()
    {
        _textTapToStart = _tapToStart.GetText();
    }

    private void Update()
    {
        FlashingText();
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

    private void FlashingText()
    {
        _timer += Time.deltaTime;

        if (_timer >= 0.5f)
        {
            _textTapToStart.enabled = true;
        }
        if (_timer >= 1f)
        {
            _textTapToStart.enabled = false;
            _timer = 0;
        }
    }
}
