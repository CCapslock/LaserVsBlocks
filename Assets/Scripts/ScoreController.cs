using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text CurrentScore;
	public Text BestScore;
	public int NumForBalanceChecking;
	public int CurrentLvl = 0;

	private MainGameController _gameController;
	private UIController _uiController;
	private float _score;
	private float _bestScore;
	private int _numFromLastChecking = 0;

	private void Start()
	{
		_bestScore = PlayerPrefs.GetFloat("BestScore");
		_gameController = GetComponent<MainGameController>();
		_uiController = FindObjectOfType<UIController>();
	}
	//добавляет очки за уничтожение блока лазером
	public void AddScore(float AddingScore)
	{
		_score += AddingScore;

		if (_score > _bestScore)
		{
			_bestScore = _score;
		}

		UpdateScore();

		if (_score >= _numFromLastChecking + NumForBalanceChecking)
		{
			_gameController.CheckForLvlUp((int)_score);
			_numFromLastChecking += NumForBalanceChecking;
		}

	}
	//добавляет очки за линии
	public void AddScoreFromLine(float AddingScore)
	{
		_score += AddingScore * 2 * CurrentLvl;
		if (_score > PlayerPrefs.GetFloat("BestScore"))
		{
			_bestScore = _score;
		}

		UpdateScore();

		if (_score >= _numFromLastChecking + NumForBalanceChecking)
		{
			_gameController.CheckForLvlUp((int)_score);
			_numFromLastChecking += NumForBalanceChecking;
		}

	}
	// Обновление количества очков в UI
	public void UpdateScore()
	{
		_uiController.UpdateScoreUI(_score, _bestScore);
	}

	//запоминает лучший рекорд
	public void RememberTheScore()
	{
		if (_score > PlayerPrefs.GetFloat("BestScore"))
		{
			PlayerPrefs.SetFloat("BestScore", _score);
		}
	}

	public float GetCurrentScore()
	{
		return _score;
	}
	public float GetBestScore()
	{
		return _bestScore;
	}
}
