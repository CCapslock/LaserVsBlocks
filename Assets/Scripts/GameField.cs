using UnityEngine;

public class GameField : MonoBehaviour
{
	public int CurrentLvl;

	private PlaceAbleBlock[] _blocksOnScene;
	private PlaceAbleBlock[,] GameFieldBlocks;
	private PlaceAbleBlock[,] GameFieldBlocksForDestruction;
	private MainGameController _gameController;
	private PopingScoreController _popingController;
	private float _destroyingLineYCoordinat; 
	private int _column = 7;
	private int _row = 15;
	private int _minRow = 15;

	private void Start()
	{
		_gameController = GetComponent<MainGameController>();
		_popingController = FindObjectOfType<PopingScoreController>();
		GameFieldBlocksForDestruction = new PlaceAbleBlock[_row, _column]; 
	}
	//конвертирует ячейки на поле в двумерный массив 
	public void ConvertPlaceAbleObjectsIntoArray(int Row, int Column)
	{
		_row = Row;
		_column = Column;
		_blocksOnScene = FindObjectsOfType<PlaceAbleBlock>();
		GameFieldBlocks = new PlaceAbleBlock[_row, _column];
		for (int i = 0; i < _row; i++)
		{
			float MinNum = 3000;
			for (int j = 0; j < _blocksOnScene.Length; j++)
			{
				if (_blocksOnScene[j] != null && _blocksOnScene[j].transform.position.y < MinNum)
					MinNum = _blocksOnScene[j].transform.position.y;
			}
			int YCounter = 0;
			for (int k = 0; k < _blocksOnScene.Length; k++)
			{
				if (_blocksOnScene[k] != null && _blocksOnScene[k].transform.position.y == MinNum)
				{
					GameFieldBlocks[i, YCounter] = _blocksOnScene[k];
					_blocksOnScene[k] = null;
					YCounter++;
				}
			}
			for (int u = 0; u < _column; u++)
			{
				for (int m = 0; m < _column; m++)
				{
					if (GameFieldBlocks[i, u].transform.position.x < GameFieldBlocks[i, m].transform.position.x)
					{
						PlaceAbleBlock temp = GameFieldBlocks[i, u];
						GameFieldBlocks[i, u] = GameFieldBlocks[i, m];
						GameFieldBlocks[i, m] = temp;

					}
				}
			}
		}
		for (int i = 0; i < _row; i++)
		{
			for (int j = 0; j < _column; j++)
			{
				GameFieldBlocks[i, j].RowNum = i;
				GameFieldBlocks[i, j].ColumnNum = j;
			}
		}
	}
	//проверяет собралась ли линия
	public void CheckForConnection()
	{
		bool GotLine = false;
		_minRow = 0;
		//Check Horizontal Lines
		for (int i = 0; i < _row; i++)
		{
			int Counter = 0;
			for (int k = 0; k < _column; k++)
			{
				if (GameFieldBlocks[i, k].IsBusy)
					Counter++;
				if (Counter == _column)
				{
					if(_minRow == 0)
					{
						_minRow = i;
					}
					for (int m = 0; m < _column; m++)
					{
						GameFieldBlocksForDestruction[i, m] = GameFieldBlocks[i, m];
					}
					GotLine = true;
				}

			}
		}
		float PointsCounter = 0;
		if (GotLine)
		{
			for (int m = 0; m < _column; m++)
			{
				for (int i = 0; i < _row; i++)
				{
					if (GameFieldBlocksForDestruction[i, m] != null)
					{
						PointsCounter += GameFieldBlocksForDestruction[i, m].PlacedBlock.HealthPoints;
						_destroyingLineYCoordinat = GameFieldBlocksForDestruction[i, m].PlacedBlock.BlockTransform.position.y;
						GameFieldBlocksForDestruction[i, m].PlacedBlock.DestroyBlock();
						GameFieldBlocksForDestruction[i, m].SetFree();
						GameFieldBlocksForDestruction[i, m] = null;
					}
				}
			}
			_gameController.AddScore(PointsCounter, true);
			_popingController.PopScore(_destroyingLineYCoordinat, PointsCounter * 2 * CurrentLvl);
			ActivateSingleBlocks();
		}
	}
	//проверяет провалился ли игрок
	public bool IsPlayerFail()
	{

		int Counter = 0;
		for (int k = 0; k < _column; k++)
		{
			if (GameFieldBlocks[_row - 1, k].IsBusy)
				Counter++;
		}
		if (Counter > 0)
		{
			return true;
		}
		else
		{
			return false;
		}

	}
	private void ActivateSingleBlocks()
	{
		int counter = 0;
		for (int m = _minRow; m < _row; m++)
		{
			for (int i = 0; i < _column; i++)
			{
				if (GameFieldBlocks[m, i].IsBusy)
				{
					GameFieldBlocks[m, i].PlacedBlock.ActivateBlock();
					GameFieldBlocks[m, i].SetFree();
					counter++;
				}
			}
		}
	}
	//проверяет занята ли ячейка
	public bool IsBlockBusy(int RowNum, int ColumnNum)
	{
		return GameFieldBlocks[RowNum, ColumnNum].IsBusy;
	}
}
