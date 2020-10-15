using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private MainMenu _mainMenu;
    private InGameUI _inGameUI;
    private PauseMenu _pauseMenu;
    private EndGameMenu _endGameMenu;

    private MainGameController _mainGameController;

    private void Start()
    {
        Time.timeScale = 1;

        _mainMenu = GetComponentInChildren<MainMenu>();
        _inGameUI = GetComponentInChildren<InGameUI>();
        _pauseMenu = GetComponentInChildren<PauseMenu>();
        _endGameMenu = GetComponentInChildren<EndGameMenu>();

        _mainGameController = FindObjectOfType<MainGameController>();

        SwitchUI(UIState.MainMenu);
    }

    public void SwitchUI (UIState state)
    {
        switch (state)
        {
            case UIState.MainMenu:
                _mainMenu.Show();
                _inGameUI.Hide();
                _pauseMenu.Hide();
                _endGameMenu.Hide();
                break;
            case UIState.InGame:
                _mainMenu.Hide();
                _inGameUI.Show();
                _pauseMenu.Hide();
                _endGameMenu.Hide();
                break;
            case UIState.Pause:
                _mainMenu.Hide();
                _inGameUI.Hide();
                _pauseMenu.Show();
                _endGameMenu.Hide();
                break;
            case UIState.EndGame:
                _mainMenu.Hide();
                _inGameUI.Hide();
                _pauseMenu.Hide();
                _endGameMenu.Show();
                break;
        }
    }

    public void StartGame()
    {
        SwitchUI(UIState.InGame);
        _mainGameController.StartGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        SwitchUI(UIState.Pause);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        SwitchUI(UIState.InGame);
    }
    public void RestartGame()
    {
        _mainGameController.RestartGame();
    }
    public void UpdateScoreUI(float currentScore, float bestScore)
    {
        _inGameUI.UpdateScore(currentScore, bestScore);
    }
}
