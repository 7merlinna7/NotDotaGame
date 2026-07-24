using UnityEngine;

public class SnowMound : MonoBehaviour, ISmashable
{
    [SerializeField] private Transform _objectSpawnPoint;
    private Vector3 _spawnPointUpPosition = new Vector3(0,3,0);
    private int _smashCount = 1;

    public void Smash()
    {
        if (_smashCount > 0)
        {
            _smashCount--;
            _objectSpawnPoint.position += _spawnPointUpPosition;
        }
    }
}
