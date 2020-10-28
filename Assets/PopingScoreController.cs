using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PopingScoreController : MonoBehaviour
{
	public GameObject PopingOutScore;
	public float SpeedOfMovingUp;
	public float TimeOfShowingScore;

	private Text[] _scoreText;
	private Vector3 _popingPoint;
	private Transform _scoreObjectTRansform;
	private bool _needToMoveUp;

	public void PopScore(float YCoordinat, float Score)
	{
		_popingPoint.y = YCoordinat;

		GameObject ScoreObject = Instantiate(PopingOutScore, _popingPoint, Quaternion.identity);
		_scoreObjectTRansform = ScoreObject.transform;
		_scoreText = ScoreObject.GetComponentsInChildren<Text>();
		for (int i = 0; i < 2; i++)
		{
			_scoreText[i].text = (math.round(Score)).ToString();
		}
		Invoke("DestroyPopedScore", TimeOfShowingScore);
		_needToMoveUp = true;
	}
	private void FixedUpdate()
	{
		if (_needToMoveUp)
		{
			_popingPoint.y = _scoreObjectTRansform.transform.position.y + SpeedOfMovingUp;
			_scoreObjectTRansform.position = _popingPoint;
		}
	}
	private void DestroyPopedScore()
	{
		_needToMoveUp = false;
		Destroy(_scoreObjectTRansform.gameObject);
	}
}
