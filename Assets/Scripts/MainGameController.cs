using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour
{
	public SingleBlock[] BlocksForMovement;
	public BalancePreset CurrentBalancePreset;
	public int Column = 7;
	public int Row = 15;

	private SingleBlock[] _singleBlocksInGame;
	private SceneCreator _sceneCreator;
	private GameField _gameField;
	private SingleBlocksMovementController _blocksMovementController;
	private ScoreController _scoreController;
	private SpawnController _spawnController;
	private BalanceController _balanceController;
	private bool _blocksIsMoving;
	private bool _needChecking;
	private void Awake()
	{
		_sceneCreator = GetComponent<SceneCreator>();
		_spawnController = GetComponent<SpawnController>();
		_blocksMovementController = GetComponent<SingleBlocksMovementController>();
		_scoreController = GetComponent<ScoreController>();
		_gameField = GetComponent<GameField>();
		_balanceController = new BalanceController();

		//настройка BalanceController'а и стартового баланса
		_balanceController.GameController = this;
		_balanceController.CurrentBalancePreset = CurrentBalancePreset;
		_balanceController.SetCorrectBalance(0);
		_scoreController.NumForBalanceChecking = CurrentBalancePreset.DifferenceBetweenLvls + 1;
		SetCorrectBalance();

		//настройка сцены и SpawnController'а
		_sceneCreator.BuildScene(Row, Column);
		_spawnController.CreateSpawnPoints(_sceneCreator.CreateSpawnPoints(Row, Column), Column, Row);
		_spawnController.GlobalMaxHp = CurrentBalancePreset.GlobalMaxHp;
		_spawnController.CreatePoolOfSingleBlocks();
	}
	private void Start()
	{
		_gameField.ConvertPlaceAbleObjectsIntoArray(Row, Column);
		_singleBlocksInGame = new SingleBlock[Column * 15];
		_singleBlocksInGame = FindObjectsOfType<SingleBlock>();
		_blocksMovementController.FindAllSingleBlocks(_spawnController.GetSingleBlocksFromPool());
	}

	public void StartGame()
    {
		SpawnFigure();
    }
	//проверяет нужно ли спавнить фигуру
	public void CheckForSpawning()
	{
		CheckForFail();
		if (_needChecking)
		{
			_blocksIsMoving = false;
			for (int i = 0; i < _singleBlocksInGame.Length && _blocksIsMoving == false; i++)
			{
				if (_singleBlocksInGame[i] != null && _singleBlocksInGame[i].CanMove)
				{
					_blocksIsMoving = true;
				}
			}
			if (!_blocksIsMoving)
			{
				_needChecking = false;
				_gameField.CheckForConnection();
				SpawnFigure();
			}
		}
	}
	//проверяет проиграл ли игрок
	private void CheckForFail()
	{
		if (_gameField.IsPlayerFail())
		{
			RestartGame();
		}
	}
	//перезапускает игру
	public void RestartGame()
	{
		_scoreController.RememberTheScore();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	//спавнит фигуру
	private void SpawnFigure()
	{
		_spawnController.SpawnFigure();
		_needChecking = true;
	}
	public void SetCorrectBalance()
	{
		_spawnController.SetBlockPatterns(_balanceController.GetFigures());
		_spawnController.SetHpRange(_balanceController.GetMinHp(), _balanceController.GetMaxHp());
	}
	//добавляет очки
	public void AddScore(float AddingScore)
	{
		_scoreController.AddScore(AddingScore);
	}
	//возвращает блок в массив
	public void ReturnBlockIntoPool(GameObject Block)
	{
		_spawnController.ReturnBlockIntoPool(Block);
	}
	// проверяет не пора ли повысить сложность
	public void CheckForLvlUp(int CurrentScore)
	{
		_balanceController.SetCorrectBalance(CurrentScore);
	}
}
