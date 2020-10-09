using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text CurrentScore;
	public Text BestScore;
	private float Score;

	private void Start()
	{
		BestScore.text = "best: " + Mathf.Floor(PlayerPrefs.GetFloat("BestScore")).ToString();
		CurrentScore.text = "0";
	}
	//добавляет очки
	public void AddScore(float AddingScore)
	{
		Score += AddingScore;
		if (Score > PlayerPrefs.GetFloat("BestScore"))
		{
			BestScore.text = "best: " + (Mathf.Floor(Score)).ToString();
		}
		CurrentScore.text = (Mathf.Floor(Score)).ToString();
	}
	//запоминает лучший рекорд
	public void RememberTheScore()
	{
		if (Score > PlayerPrefs.GetFloat("BestScore"))
		{
			PlayerPrefs.SetFloat("BestScore", Score);
		}
	}
}
