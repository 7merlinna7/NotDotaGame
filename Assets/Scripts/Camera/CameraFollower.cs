using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _offcet;
    private Vector3 _playerPositionXZ;

    private void Update()
    {
        _playerPositionXZ = new Vector3 (_player.transform.position.x,0, _player.transform.position.z);
        transform.position = _playerPositionXZ + _offcet;
    }
}
