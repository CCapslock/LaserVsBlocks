using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject _singleBlockPrefab;
    public SingleBlock _singleBlockForActivation;
    public CreateFigure[] _blockPatterns;

    private GameObject[] _blocksGameObjects;
    private GameObject[] _singleBlockPool;
    private Vector2[,] _spawnPoints;
    private Vector2 _spawnPosition;
    private Vector2 _farPosition;
    private Vector2 _notfarPosition;
    private int _counter;
    private int _columnNum;
    private int _column;
    private int _row;

    //получает спавн поинты из SceneCreator
    public void CreateSpawnPoints(Vector2[,] SpawnPoints, int Column, int Row)
    {
        _spawnPoints = SpawnPoints;
        _blocksGameObjects = new GameObject[20];
        _spawnPosition = new Vector2();
        _column = Column;
        _row = Row;
    }
    //спавнит фигуры
    public void SpawnFigure()
    {
        switch (_counter)
        {
            case 0:
                _columnNum = 1;
                _counter++;
                break;
            case 1:
                _columnNum = 3;
                _counter++;
                break;
            case 2:
                _columnNum = 5;
                _counter = 0;
                break;
        }
        CreateFigure RandomFigure = _blockPatterns[Random.Range(0, _blockPatterns.Length)];
        for (int i = 0; i < RandomFigure.SingleBlocks.Length; i++)
        {
            for (int j = 0; j < _singleBlockPool.Length; j++)
            {
                if(_singleBlockPool[j] != null)
                {
                    _blocksGameObjects[i] = _singleBlockPool[j];
                    _singleBlockPool[j] = null;
                    break;
                }
            }
            _spawnPosition = _spawnPoints[RandomFigure.SingleBlocks[i].YCordinat, _columnNum + RandomFigure.SingleBlocks[i].XCordinat];
            _blocksGameObjects[i].transform.position = _spawnPosition;
            _singleBlockForActivation = _blocksGameObjects[i].GetComponent<SingleBlock>();
            _singleBlockForActivation.ActivateBlockForSpawn();

            //_blocksGameObjects[i] = Instantiate(_singleBlockPrefab, _spawnPoints[RandomFigure.SingleBlocks[i].YCordinat, _columnNum + RandomFigure.SingleBlocks[i].XCordinat],Quaternion.identity);
        }
    }
    //передаем через этот метод массив сингблоков в мувментКонтроллер
    public SingleBlock[] GetSingleBlocksFromPool()
    {
        SingleBlock[] _singleBlocksForMovement = new SingleBlock[_singleBlockPool.Length];
        for (int i = 0; i < _singleBlockPool.Length; i++)
        {
            _singleBlocksForMovement[i] = _singleBlockPool[i].GetComponent<SingleBlock>();
        }
        return _singleBlocksForMovement;
    }
    //создает пул блоков
    public void CreatePoolOfSingleBlocks()
    {
        _farPosition = new Vector2(10, 10);
        _notfarPosition = new Vector2(9, 9);
        GameObject SingleBlockForChanging = Instantiate(_singleBlockPrefab, _farPosition, Quaternion.identity);
        SingleBlockForChanging = ScaleBlock(SingleBlockForChanging);
        _singleBlockPool = new GameObject[_column * (_row - 1)];
        _singleBlockPool[0] = SingleBlockForChanging;
        for (int i = 1; i < _column * (_row - 1); i++)
        {
            _singleBlockPool[i] = Instantiate(SingleBlockForChanging, _farPosition, Quaternion.identity);
        }

    }
    //изменяет размер Блока перед созданием пула
    private GameObject ScaleBlock(GameObject Block)
    {
        return Block;
    }
    //возвращает блок в пул
    public void ReturnBlockIntoPool(GameObject Block)
    {
        SingleBlock BlockScript = Block.GetComponent<SingleBlock>();
        for (int j = 0; j < _singleBlockPool.Length; j++)
        {
            if (_singleBlockPool[j] == null)
            {
                _singleBlockPool[j] = Block;
                break;
            }
        }
        Block.transform.position = _notfarPosition;
    }
}
