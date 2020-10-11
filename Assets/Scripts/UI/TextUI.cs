using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    private Text _textUI;

    public void SetText(string text)
    {
        _textUI.GetComponent<Text>().text = text;
    }
}
