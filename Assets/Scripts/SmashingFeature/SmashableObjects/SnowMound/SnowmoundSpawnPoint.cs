using UnityEngine;

public class SnowmoundSpawnPoint : MonoBehaviour 
{
    [SerializeField] GiftBox _giftBoxPrefub;
    [SerializeField] Bomb _bombPrefab;
    [SerializeField] Transform _objectSpawnPoint;

    private bool _isObjectSpawned = false;
    private GameObject _spawnObject;

    public bool IsObjectSpawned { get => _isObjectSpawned;}

    public void SpawnObject(bool spawnGiftBox)
    {
        if (spawnGiftBox)
        {
            _isObjectSpawned = true;
            _spawnObject = _giftBoxPrefub.gameObject;
        }
        else
        {
            _spawnObject = _bombPrefab.gameObject;
        }

        if (_spawnObject != null)
        Instantiate(_spawnObject, _objectSpawnPoint.position, _objectSpawnPoint.rotation, _objectSpawnPoint);
    }
}
