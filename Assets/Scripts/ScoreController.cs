using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text CurrentScore;
	public Text BestScore;
	public int NumForBalanceChecking = 501;

	private MainGameController _gameController;
	private float _score;
	private int _numFromLastChecking = 0;

	private void Start()
	{
		BestScore.text = "best: " + Mathf.Floor(PlayerPrefs.GetFloat("BestScore")).ToString();
		CurrentScore.text = "0";
		_gameController = GetComponent<MainGameController>();
	}
	//добавляет очки
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
			Debug.Log("check");
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
