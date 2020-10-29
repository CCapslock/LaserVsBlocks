using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PopingScoreController : MonoBehaviour
{
	public GameObject PopingOutScore;
	public Animator AnimatorController;
	public float SpeedOfMovingUp;
	public float TimeOfShowingScore;

	private Text[] _scoreText;
	private Vector3 _popingPoint;
	private Vector3 _outVector;
	private Transform _scoreObjectTRansform;
	private bool _needToMoveUp;

	private void Start()
	{
		
	}
	public void PopScore(float YCoordinat, float Score)
	{
		_popingPoint.y = YCoordinat;
		PopingOutScore.transform.position = _popingPoint;
		_scoreObjectTRansform = PopingOutScore.transform;
		_scoreText = PopingOutScore.GetComponentsInChildren<Text>();
		for (int i = 0; i < 2; i++)
		{
			_scoreText[i].text = (math.round(Score)).ToString();
		}
		AnimatorController.SetTrigger("Pop");
		_needToMoveUp = true;
		Invoke("DestroyPopedScore", TimeOfShowingScore);
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
	}
}
