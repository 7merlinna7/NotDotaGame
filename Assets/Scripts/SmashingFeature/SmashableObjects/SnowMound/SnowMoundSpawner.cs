using System.Collections.Generic;
using UnityEngine;

public class SnowMoundSpawner : MonoBehaviour
{
    [SerializeField] private List<SnowmoundSpawnPoint> _spawnPoints;
    [SerializeField] private int _objectSpawnCount;
    private int _currentObjectSpawnCount;

    private void Awake()
    {
       SpawnObjects(); 
    }

    private void SpawnObjects ()
    {
        while(_currentObjectSpawnCount < _objectSpawnCount)
        {
            int objectSpawnNumber = Random.Range(0, _spawnPoints.Count);

            if (_spawnPoints[objectSpawnNumber].IsObjectSpawned == false)
                _spawnPoints[objectSpawnNumber].SpawnObject(true);

            _currentObjectSpawnCount++;
        }

        foreach (SnowmoundSpawnPoint snowmoundSpawnPoint in _spawnPoints)
        {
            if(snowmoundSpawnPoint.IsObjectSpawned == false)
                snowmoundSpawnPoint.SpawnObject(false);
        }
    }
}
