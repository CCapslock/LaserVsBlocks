using UnityEngine;
using UnityEngine.UI;

public class SingleBlock : MonoBehaviour
{
	public GameObject BlockGlow;
	public Transform BlockTransform;
	public Gradient HealthGradient;
	public ParticleSystem DestroyParticle;
	public LayerMask SingleBlockLayer;
	public float MaxHealthPoint;
	public float HealthPoints;
	public float SideRaycastLength;
	public float LaserPower;
	public bool CanMove;

	private RaycastHit2D _raycastHit;
	private SingleBlock _anotherSingleBlock;
	private PlaceAbleBlock _placeAbleBlock;
	private GameField _gameField;
	private Collider2D _blockCollider;
	private SpriteRenderer _blockSpriteRenderer;
	private MainGameController _gameController;
	private Text _healthPointsText;
	private Vector2 _destroyPosition;
	private bool CanGetDamage = true;

	private void Awake()
	{
		BlockTransform = GetComponent<Transform>();
		_gameField = FindObjectOfType<GameField>();
		_blockCollider = GetComponent<Collider2D>();
		_healthPointsText = gameObject.GetComponentInChildren<Text>();
		_blockSpriteRenderer = GetComponent<SpriteRenderer>();
		_blockSpriteRenderer.color = HealthGradient.Evaluate(Mathf.Floor(HealthPoints) / MaxHealthPoint);
		_gameController = FindObjectOfType<MainGameController>();
		_destroyPosition = new Vector2();
		DestroyParticle.Stop();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("PlaceAbleBlock"))
		{
			_placeAbleBlock = other.gameObject.GetComponent<PlaceAbleBlock>();
			CheckForPlacement();
		}
	}
	//проверяет стоит ли ставить блок
	private void CheckForPlacement()
	{
		if (!_placeAbleBlock.IsBusy)
		{
			if (_placeAbleBlock.RowNum != 0)
			{
				if (_gameField.IsBlockBusy(_placeAbleBlock.RowNum - 1, _placeAbleBlock.ColumnNum))
				{
					CanGetDamage = false;
					Invoke("PlaceBlock", 0.11f);//invoke использую что бы все блоки успели сколайдится с нужным колайдером
				}
			}
			else
			{
				CanGetDamage = false;
				Invoke("PlaceBlock", 0.11f);
			}
		}
	}
	//ставит блок в ячейку
	public void PlaceBlock()
	{
		if (_placeAbleBlock != null)
		{
			_placeAbleBlock.SetBusy(this);
			CanMove = false;
			_blockCollider.enabled = false;
			PlaceOtherBlocks();
			BlockTransform.position = _placeAbleBlock.transform.position;
			Invoke("CheckForSpawning", 0.1f);//костыльчик
		}
		else
		{
			_gameController.CheckForSpawning();
		}

	}
	private void CheckForSpawning()
	{
		_gameController.CheckForSpawning();
	}
	//активирует блок для спавна
	public void ActivateBlockForSpawn(float MinHp, float MaxHp)
	{
		if (_blockCollider != null)
		{
			_blockCollider.enabled = true;
		}
		HealthPoints = Random.Range(MinHp, MaxHp);
		_blockSpriteRenderer.color = HealthGradient.Evaluate(Mathf.Floor(HealthPoints) / MaxHealthPoint);
		CanGetDamage = true;
		_healthPointsText.text = (Mathf.Floor(HealthPoints)).ToString();
		CanMove = true;
		SetEffectOn(false);
	}
	//активирует блок
	public void ActivateBlock()
	{
		if (_blockCollider != null)
		{
			_blockCollider.enabled = true;
		}
		CanMove = true;
		CanGetDamage = true;
	}
	//говорит соседним блокам что пора в ячейку встать
	private void PlaceOtherBlocks()
	{
		_raycastHit = Physics2D.Raycast(BlockTransform.position, BlockTransform.up, SideRaycastLength, SingleBlockLayer);
		if (_raycastHit.collider != null)
		{
			_anotherSingleBlock = _raycastHit.collider.gameObject.GetComponent<SingleBlock>();
			if (_anotherSingleBlock.CanMove)
				_anotherSingleBlock.PlaceBlock();
		}
		_raycastHit = Physics2D.Raycast(BlockTransform.position, BlockTransform.up, -SideRaycastLength, SingleBlockLayer);
		if (_raycastHit.collider != null)
		{
			_anotherSingleBlock = _raycastHit.collider.gameObject.GetComponent<SingleBlock>();
			if (_anotherSingleBlock.CanMove)
				_anotherSingleBlock.PlaceBlock();
		}
		_raycastHit = Physics2D.Raycast(BlockTransform.position, BlockTransform.right, SideRaycastLength, SingleBlockLayer);
		if (_raycastHit.collider != null)
		{
			_anotherSingleBlock = _raycastHit.collider.gameObject.GetComponent<SingleBlock>();
			if (_anotherSingleBlock.CanMove)
				_anotherSingleBlock.PlaceBlock();
		}
		_raycastHit = Physics2D.Raycast(BlockTransform.position, BlockTransform.right, -SideRaycastLength, SingleBlockLayer);
		if (_raycastHit.collider != null)
		{
			_anotherSingleBlock = _raycastHit.collider.gameObject.GetComponent<SingleBlock>();
			if (_anotherSingleBlock.CanMove)
				_anotherSingleBlock.PlaceBlock();
		}
	}
	//получает урон
	public void GetDamage()
	{
		if (CanGetDamage)
		{
			HealthPoints -= LaserPower;
			_gameController.AddScore(0.4f);
			if (HealthPoints <= 1)
			{
				DestroyBlock();
			}
			_healthPointsText.text = (Mathf.Floor(HealthPoints)).ToString();
			_blockSpriteRenderer.color = HealthGradient.Evaluate(Mathf.Floor(HealthPoints) / MaxHealthPoint);
		}
	}
	public void SetEffectOn(bool NeedToOn)
	{
		BlockGlow.SetActive(NeedToOn);
	}
	public void DestroyBlock()
	{
		_destroyPosition = BlockTransform.position;
		CanMove = false;
		_gameController.ReturnBlockIntoPool(this.gameObject);
		_gameController.CheckForSpawning();
		Invoke("PlayDestroyParticles", 0.1f);
		//Destroy(this.gameObject);
	}
	private void PlayDestroyParticles()
	{
		DestroyParticle.transform.position = _destroyPosition;
		DestroyParticle.Play();
		Invoke("ReturnParticleSystem", 0.01f);
	}
	private void ReturnParticleSystem()
	{
		DestroyParticle.transform.position = BlockTransform.position;
	}
}
