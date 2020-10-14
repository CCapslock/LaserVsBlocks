using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text CurrentScore;
	public Text BestScore;
	public int NumForBalanceChecking;
	public int CurrentLvl = 0;

	private MainGameController _gameController;
	private float _score;
	private int _numFromLastChecking = 0;

	private void Start()
	{
		BestScore.text = "best: " + Mathf.Floor(PlayerPrefs.GetFloat("BestScore")).ToString();
		CurrentScore.text = "0";
		_gameController = GetComponent<MainGameController>();
	}
	//добавляет очки за уничтожение блока лазером
	public void AddScore(float AddingScore)
	{
		_score += AddingScore;
		if (_score > PlayerPrefs.GetFloat("BestScore"))
		{
			BestScore.text = "best: " + (Mathf.Floor(_score)).ToString();
		}
		CurrentScore.text = (Mathf.Floor(_score)).ToString();
		if(_score >= _numFromLastChecking + NumForBalanceChecking)
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
			BestScore.text = "best: " + (Mathf.Floor(_score)).ToString();
		}
		CurrentScore.text = (Mathf.Floor(_score)).ToString();
		if (_score >= _numFromLastChecking + NumForBalanceChecking)
		{
			_gameController.CheckForLvlUp((int)_score);
			_numFromLastChecking += NumForBalanceChecking;
		}

	}
	//запоминает лучший рекорд
	public void RememberTheScore()
	{
		if (_score > PlayerPrefs.GetFloat("BestScore"))
		{
			PlayerPrefs.SetFloat("BestScore", _score);
		}
	}
}
