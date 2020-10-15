using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    private Button _button;
    private Text _buttonText;

    public Button GetControl
    {
        get
        {
            if (!_button)
            {
                _button = transform.GetComponent<Button>();
            }
            return _button;
        }
    }

    public void SetText(string text)
    {
        if (!_buttonText)
        {
            _buttonText = transform.GetComponentInChildren<Text>();
        }
        _buttonText.text = text;
    }

    public Text GetText()
    {
        return transform.GetComponentInChildren<Text>();
    }
}
