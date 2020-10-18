using UnityEngine;

public class SceneCreator : MonoBehaviour
{
	public GameObject PlaceAbleObjectPrefab;
	public GameObject SingleBlockPrefab;
	private Camera _camera;
	private Vector2[,] _spawnPoints;
	private Vector2 _minScreenPosition;
	private Vector2 _maxScreenPosition;
	private float _widht;
	private float _hight;
	private int _row;
	private int _column;
	//расставляет ячейки на поле
	public void BuildScene(int Row, int Column)
	{
		_camera = FindObjectOfType<Camera>();
		_minScreenPosition = _camera.ViewportToWorldPoint(new Vector2(0, 0));
		_maxScreenPosition = _camera.ViewportToWorldPoint(new Vector2(1, 1));
		_widht = _maxScreenPosition.x * 2;
		_hight = _maxScreenPosition.y * 2;
		_row = Row;
		_column = Column;
		float xnum = _widht / (_column + 1);
		float ynum = _hight / (_row + 1);
		SpriteRenderer PlaceAbleBlockSprite = PlaceAbleObjectPrefab.GetComponent<SpriteRenderer>();
		Debug.Log("PlaceAbleBlock Size: " + PlaceAbleBlockSprite.sprite.bounds.size.y * PlaceAbleObjectPrefab.transform.localScale.y + " Screen Hight: " + _maxScreenPosition.y);
		for (int i = 1; i <= Row; i++)
		{
			for (int j = 1; j <= Column; j++)
			{
				Vector2 pposition = new Vector2(_minScreenPosition.x + xnum * j, _minScreenPosition.y + ynum * i);
				Instantiate(PlaceAbleObjectPrefab, pposition, Quaternion.identity);
			}
		}
	}
	//создает точки спавна для SpawnerController
	public Vector2[,] CreateSpawnPoints(int Row, int Column)
	{
		_spawnPoints = new Vector2[3, Column];
		float xnum = _widht / (Column + 1);
		float ynum = _hight / (Row + 1);
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < Column; j++)
			{
				_spawnPoints[i, j].x = _minScreenPosition.x + xnum * (j + 1);
				_spawnPoints[i, j].y = _maxScreenPosition.y + ynum * (i + 1);
			}
		}
		return _spawnPoints;
	}
}
