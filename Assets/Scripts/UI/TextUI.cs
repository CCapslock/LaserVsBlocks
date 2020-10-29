using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    private Text _textUI;

    private void Awake()
    {
        _textUI = GetComponent<Text>();
    }

    public void SetText(string text)
    {
        _textUI.text = text;
    }
}
