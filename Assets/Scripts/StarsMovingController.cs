using System;
using System.Linq.Expressions;
using UnityEngine;

public class StarsMovingController : MonoBehaviour
{
    public float[] StarsMovingSpeeds;
    public GameObject FirstLayer;
    public GameObject SecondLayer;
    public GameObject ThirdLayer;
    public GameObject FourthLayer;

    private GameObject _firstLayer;
    private GameObject _secondLayer;
    private GameObject _thirdLayer;
    private GameObject _fourthLayer;

    private GameObject _firstLayer2;
    private GameObject _secondLayer2;
    private GameObject _thirdLayer2;
    private GameObject _fourthLayer2;

    private Vector3 _movingVector;
    private Vector3 _secondLayerSpawnPoint;

    private void Start()
    {
        _secondLayerSpawnPoint = new Vector2(0, 23);

        _firstLayer = Instantiate(FirstLayer, new Vector2(), Quaternion.identity);
        _secondLayer = Instantiate(SecondLayer, new Vector2(), Quaternion.identity);
        _thirdLayer = Instantiate(ThirdLayer, new Vector2(), Quaternion.identity);
        _fourthLayer =  Instantiate(FourthLayer, new Vector2(), Quaternion.identity);

        _firstLayer2 = Instantiate(FirstLayer, _secondLayerSpawnPoint, Quaternion.identity);
        _secondLayer2 = Instantiate(SecondLayer, _secondLayerSpawnPoint, Quaternion.identity);
        _thirdLayer2 = Instantiate(ThirdLayer, _secondLayerSpawnPoint, Quaternion.identity);
        _fourthLayer2 =  Instantiate(FourthLayer, _secondLayerSpawnPoint, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        MoveStars();
        CheckForReplacement();
    }

    private void CheckForReplacement()
    {
        if (_firstLayer.transform.position.y <= -23)
        {
            _firstLayer.transform.position = _secondLayerSpawnPoint;
        }
        if (_secondLayer.transform.position.y <= -23)
        {
            _secondLayer.transform.position = _secondLayerSpawnPoint;
        }
        if (_thirdLayer.transform.position.y <= -23)
        {
            _thirdLayer.transform.position = _secondLayerSpawnPoint;
        }
        if (_fourthLayer.transform.position.y <= -23)
        {
            _fourthLayer.transform.position = _secondLayerSpawnPoint;
        }
        if (_firstLayer2.transform.position.y <= -23)
        {
            _firstLayer2.transform.position = _secondLayerSpawnPoint;
        }
        if (_secondLayer2.transform.position.y <= -23)
        {
            _secondLayer2.transform.position = _secondLayerSpawnPoint;
        }
        if (_thirdLayer2.transform.position.y <= -23)
        {
            _thirdLayer2.transform.position = _secondLayerSpawnPoint;
        }
        if (_fourthLayer2.transform.position.y <= -23)
        {
            _fourthLayer2.transform.position = _secondLayerSpawnPoint;
        }
    }

    private void MoveStars()
    {
        _movingVector = _firstLayer.transform.position;
        _movingVector.y -= StarsMovingSpeeds[0];
        _firstLayer.transform.position = _movingVector;
        
        _movingVector = _secondLayer.transform.position;
        _movingVector.y -= StarsMovingSpeeds[1];
        _secondLayer.transform.position = _movingVector;
        
        _movingVector = _thirdLayer.transform.position;
        _movingVector.y -= StarsMovingSpeeds[2];
        _thirdLayer.transform.position = _movingVector;
        
        _movingVector = _fourthLayer.transform.position;
        _movingVector.y -= StarsMovingSpeeds[3];
        _fourthLayer.transform.position = _movingVector;
        
        _movingVector = _firstLayer2.transform.position;
        _movingVector.y -= StarsMovingSpeeds[0];
        _firstLayer2.transform.position = _movingVector;
        
        _movingVector = _secondLayer2.transform.position;
        _movingVector.y -= StarsMovingSpeeds[1];
        _secondLayer2.transform.position = _movingVector;
        
        _movingVector = _thirdLayer2.transform.position;
        _movingVector.y -= StarsMovingSpeeds[2];
        _thirdLayer2.transform.position = _movingVector;
        
        _movingVector = _fourthLayer2.transform.position;
        _movingVector.y -= StarsMovingSpeeds[3];
        _fourthLayer2.transform.position = _movingVector;

    }
}
