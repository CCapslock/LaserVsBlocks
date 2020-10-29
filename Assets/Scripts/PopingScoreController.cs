using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PopingScoreController : MonoBehaviour
{
	public GameObject PopingOutScore;
	public Animator AnimatorController;
	public float SpeedOfMovingUp;
	public float TimeOfShowingScore;

	[SerializeField] private Text[] _scoreText;
	private Vector3 _popingPoint;
	private Vector3 _outVector;
	private Transform _scoreObjectTRansform;
	private bool _needToMoveUp;

	private void Start()
	{
		_outVector = new Vector2(0, -10f);
		_popingPoint = new Vector2();
		_scoreObjectTRansform = PopingOutScore.transform;
		_scoreText = PopingOutScore.GetComponentsInChildren<Text>(); for (int i = 0; i < 2; i++)
		{
			_scoreText[i].text = (math.round(50f)).ToString();
		}
	}
	public void PopScore(float YCoordinat, float Score)
	{
		PopingOutScore.SetActive(true);
		_popingPoint.y = YCoordinat;
		_scoreObjectTRansform.position = _popingPoint;
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
		_scoreObjectTRansform.position = _outVector;
		_needToMoveUp = false;
		Invoke("SetObjectNonActive", 1f);
	}
	private void SetObjectNonActive()
	{
		if (!_needToMoveUp)
		{
			PopingOutScore.SetActive(false);
		}
	}
}
