using UnityEngine;

public class SingleBlocksMovementController : MonoBehaviour
{
	public float Speed;

	private SingleBlock[] _singleBlocks;
	private Transform[] _singleBlockTransforms;
	private Vector3 _movingVector;

	private void Start()
	{
		_movingVector = new Vector3();
	}
	//получает синглблоки для перемещения
	public void FindAllSingleBlocks(SingleBlock[] BlocksForMoving)
	{
		_singleBlocks = new SingleBlock[BlocksForMoving.Length];
		_singleBlocks = BlocksForMoving;
		GetTransforms();
	}
	//получаем трансформы синглблоков
	private void GetTransforms()
	{
		_singleBlockTransforms = new Transform[_singleBlocks.Length];
		for (int i = 0; i < _singleBlocks.Length; i++)
		{
			if (_singleBlocks[i] != null)
			{
				_singleBlockTransforms[i] = _singleBlocks[i].transform;
			}
		}
	}
	private void FixedUpdate()
	{
		MoveBlocks();
	}
	//двигаем блоки
	private void MoveBlocks()
	{
		for (int i = 0; i < _singleBlocks.Length; i++)
		{
			if (_singleBlocks[i].CanMove)
			{
				_movingVector = _singleBlockTransforms[i].position;
				_movingVector.y -= Speed;
				_singleBlockTransforms[i].position = _movingVector;
			}
		}
	}
}
