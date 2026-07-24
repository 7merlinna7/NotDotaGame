using UnityEngine;

public class PlayerInput 
{
    private const int _leftMouseButton = 0;
    private const int _rightMouseButton = 1;

    private WalkingRayChooter _walkingRayChooter;
    private Vector3 _targetPosition;

    private Player _player;
    public bool IsTakingPreparing { get; private set; }
    public Vector3 TargetPosition { get => _targetPosition; }

    public PlayerInput(Player player)
    {
        _player = player;
    }

    public void Start()
    {
        _walkingRayChooter = new WalkingRayChooter();
    }

    public void Update ()
    {
        if (_player.IsDead == false)
        {
            if (Input.GetMouseButtonDown(_rightMouseButton))
            {
                _targetPosition = _walkingRayChooter.Shoot();

                if (_targetPosition != Vector3.zero)
                {
                    _player.IsMoving = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _player.IsJumping == false)
            {
                _player.IsJumping = true;
            }
        }
    }
}
