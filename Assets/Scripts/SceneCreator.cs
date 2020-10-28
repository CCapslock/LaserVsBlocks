using UnityEngine;

public class SceneCreator : MonoBehaviour
{
	public GameObject PlaceAbleObjectPrefab;
	public GameObject SingleBlockPrefab;
	private Camera _camera;
	private Vector2[,] _spawnPoints;
	private Vector2 _minScreenPosition;
	private Vector2 _maxScreenPosition;
	private int _row;
	private int _column;
	//расставляет ячейки на поле
	public void BuildScene(int Row, int Column)
	{
		_camera = FindObjectOfType<Camera>();
		_minScreenPosition = _camera.ViewportToWorldPoint(new Vector2(0, 0));
		_maxScreenPosition = _camera.ViewportToWorldPoint(new Vector2(1, 1));
		_row = Row;
		_column = Column;
		SpriteRenderer PlaceAbleBlockSprite = PlaceAbleObjectPrefab.GetComponent<SpriteRenderer>();
		//Debug.Log("PlaceAbleBlock Size: " + PlaceAbleBlockSprite.sprite.bounds.size.y * PlaceAbleObjectPrefab.transform.localScale.y + " Screen Hight: " + _maxScreenPosition.y * 2 + " Place Taken by Blocks: " + PlaceAbleBlockSprite.sprite.bounds.size.y * PlaceAbleObjectPrefab.transform.localScale.y * _row);
		float BlockSize = PlaceAbleBlockSprite.sprite.bounds.size.x * PlaceAbleObjectPrefab.transform.localScale.x;
		float StartXPosition = 0 - BlockSize * (_column / 2);
		float StartYPosition = 0 - BlockSize * (_row / 2);
		for (int i = 0; i < _row; i++)
		{
			for (int j = 0; j < _column; j++)
			{
				Vector2 pposition = new Vector2(StartXPosition + BlockSize * j, StartYPosition + BlockSize * i);
				Instantiate(PlaceAbleObjectPrefab, pposition, Quaternion.identity);
			}
		}
	}
	//на случай если захотим менять размер блока
	private void ChangePlaceAbleBlock()
	{

	}

	//создает точки спавна для SpawnerController
	public Vector2[,] CreateSpawnPoints(int Row, int Column)
	{
		SpriteRenderer PlaceAbleBlockSprite = PlaceAbleObjectPrefab.GetComponent<SpriteRenderer>();
		float BlockSize = PlaceAbleBlockSprite.sprite.bounds.size.x * PlaceAbleObjectPrefab.transform.localScale.x;
		float StartXPosition = 0 - BlockSize * (_column / 2);
		float StartYPosition = _maxScreenPosition.y + BlockSize;
		_spawnPoints = new Vector2[3, Column];
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < Column; j++)
			{
				_spawnPoints[i, j].x = StartXPosition + BlockSize * (j);
				_spawnPoints[i, j].y = StartYPosition + BlockSize * (i);
			}
		}
		return _spawnPoints;
	}
}
