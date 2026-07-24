using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour,IDamabeble
{
    [SerializeField] private float _woundedMoveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private LayerMask _walkingLayerMask;
    [SerializeField] private ParticleSystem _movingFlagPrefab; 
    [SerializeField] private LayerMask _smashingLayerMask;

    private Rotator _rotator;
    private NavMeshAgent _agent;
    private float _normalMoveSpeed;

    private float _minPositionDifference = 0.5f;
    private SmashableRayShooter _smashableRayShooter;
    private PlayerInput _playerInput;
    private GameObject _movingFlag;

    public bool IsMoving { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsDead { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get => _maxHealth; }
    public Vector3 GetPosition { get => transform.position; }

    private void Awake()
    {
        _rotator = new Rotator(_rotationSpeed,transform);

        _smashableRayShooter = new SmashableRayShooter(this,_smashingLayerMask);

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _normalMoveSpeed = _agent.speed;

        _playerInput = new PlayerInput(this,_walkingLayerMask);
        _playerInput.Start();

        CurrentHealth = _maxHealth;
    }

    private void Update()
    {
        _playerInput.Update();

        if (IsMoving)
            IsMovingTo(_playerInput.TargetPosition);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsDead = true;
        }
    }
    public void StartJumping() => IsJumping = true;

    public void StopJumping()
    {
        _smashableRayShooter.Shoot();
        IsJumping = false;
    }

    public void StartMoving(Vector3 movePosition)
    {
        Quaternion flagRotation = Quaternion.Euler (-90,0,0);
        _movingFlag = Instantiate(_movingFlagPrefab.gameObject,movePosition,flagRotation);
        IsMoving = true;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth-= damage;
    }

    public bool IsWounded()
    {
        if (CurrentHealth / MaxHealth <= 0.5)
        {
            _agent.speed = _woundedMoveSpeed;
            return true;
        }

        _agent.speed = _normalMoveSpeed;
        return false;
    }

    public void DestroyMovingFlag()
    {
        if(_movingFlag!= null)
        Destroy(_movingFlag);
    } 

    private void IsMovingTo(Vector3 target)
    {
        if (Vector3.Magnitude(target - transform.position) <= _minPositionDifference)
        {
            IsMoving = false;
            Destroy(_movingFlag);
        }

        _agent.SetDestination(target);
        _rotator.Update(target - transform.position);
    }
}
