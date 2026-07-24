using UnityEngine;

public class PlayerInput 
{
    private const int _leftMouseButton = 0;
    private const int _rightMouseButton = 1;

    private WalkingRayChooter _walkingRayChooter;
    private LayerMask _walkingLayerMask;
    private Vector3 _targetPosition;

    private Player _player;
    public bool IsTakingPreparing { get; private set; }
    public Vector3 TargetPosition { get => _targetPosition; }

    public PlayerInput(Player player, LayerMask groundLayerMask)
    {
        _player = player;
        _walkingLayerMask = groundLayerMask;
    }

    public void Start()
    {
        _walkingRayChooter = new WalkingRayChooter(_walkingLayerMask);
    }

    public void Update ()
    {
        if (_player.IsDead == false)
        {
            if (Input.GetMouseButtonDown(_rightMouseButton))
            {
                _player.DestroyMovingFlag();
                _targetPosition = _walkingRayChooter.Shoot();

                if (_targetPosition != Vector3.zero)
                {
                    _player.StartMoving(_targetPosition);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _player.IsJumping == false)
            {
                _player.StartJumping();
            }
        }
    }
}
