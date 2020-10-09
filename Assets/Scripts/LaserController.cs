using UnityEngine;

public class LaserController : MonoBehaviour
{
	public GameObject[] LaserEffects;
	public GameObject HighGlowLaserEffects;
	public GameObject ParticleLaserEffects;
	public Transform LaserObject;
	public LayerMask BlockLayer;

	private Transform _objectTransform;
	private RaycastHit2D _raycastHit;
	private SingleBlock _blockForHiting;
	private float _laserLength;
	private Vector3 _laserScale, _laserPosition;
	private Vector2 _highGlowLaserEffectsPosition;
	private bool _effectsOn;

	private void Start()
	{
		_objectTransform = GetComponent<Transform>();
		_laserScale = new Vector3(1f, 0, 0);
		_laserPosition = new Vector3();
		_highGlowLaserEffectsPosition = new Vector2();
		TurnEffectsOn(false);
	}
	//пускает рэйкаст и растягивает лазер по длине рейкаста
	private void FixedUpdate()
	{
		_raycastHit = Physics2D.Raycast(_objectTransform.position, _objectTransform.up, 10f, BlockLayer);
		if (_raycastHit.collider != null)
		{
			if (_blockForHiting != null && _blockForHiting != _raycastHit.collider.gameObject.GetComponent<SingleBlock>())	
			{
				_blockForHiting.SetEffectOn(false); 
				_blockForHiting = _raycastHit.collider.gameObject.GetComponent<SingleBlock>();

			}
			else if (_blockForHiting == null)
			{
				_blockForHiting = _raycastHit.collider.gameObject.GetComponent<SingleBlock>();
			}
			else
			{
				_blockForHiting.GetDamage();
				_blockForHiting.SetEffectOn(true);
			}
			_laserLength = _raycastHit.distance;
			_highGlowLaserEffectsPosition = _raycastHit.point;
			HighGlowLaserEffects.transform.position = _highGlowLaserEffectsPosition;
			ParticleLaserEffects.transform.position = _highGlowLaserEffectsPosition;
			if (!_effectsOn)
			{
				TurnEffectsOn(true);
			}
		}
		else
		{
			if (_blockForHiting != null)
			{
				_blockForHiting.SetEffectOn(false);
				_blockForHiting = null;
			}
			if (_effectsOn)
			{
				TurnEffectsOn(false);
			}
			_laserLength = 0;
		}
		_laserPosition.y = _laserLength / 2;
		LaserObject.position = _objectTransform.position + _laserPosition;
		_laserScale.y = _laserLength * 7f;
		LaserObject.localScale = _laserScale;
	}
	private void TurnEffectsOn(bool NeedToOn)
	{
		for (int i = 0; i < LaserEffects.Length; i++)
		{
			LaserEffects[i].SetActive(NeedToOn);
			_effectsOn = NeedToOn;
		}
		HighGlowLaserEffects.SetActive(NeedToOn);
		ParticleLaserEffects.SetActive(NeedToOn);
	}
}
