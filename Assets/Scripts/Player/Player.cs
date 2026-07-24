using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour,IDamabeble
{
    private const int _leftMouseButton = 0;
    private const int _rightMouseButton = 1;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxHealth;

    private WalkingRayChooter _walkingRayChooter;
    private TakingRayShooter _takingRayShooter;
    private SmashableRayShooter _smashableRayShooter;
    private Rotator _rotator;
    private NavMeshAgent _agent;

    private Vector3 _targetPosition;
    private float _minPositionDifference = 0.5f;
    private bool _isTakingPreparing;
    private ITakeble _currentTakable;

    public bool IsMoving { get; set; }
    public bool IsWounded { get; set; }
    public bool IsJumping { get; set; }
    public bool IsTaking { get; set; }
    public bool IsDead { get; private set; }

    public ITakeble CurrentTakable { get => _currentTakable; }
    public Vector3 GetPosition { get => transform.position; }
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get => _maxHealth; }

    public void StopJumping()
    {
        _smashableRayShooter.Shoot();
        IsJumping = false;
    }
    public void StopTaking()
    {
        IsTaking = false;
        _currentTakable = null;
    }

    private void Awake()
    {
        _walkingRayChooter = new WalkingRayChooter();
        _takingRayShooter = new TakingRayShooter(this);
        _smashableRayShooter = new SmashableRayShooter(this);
        
        _rotator = new Rotator(_rotationSpeed,transform);
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        IsMoving = false;
        CurrentHealth = _maxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_rightMouseButton) && IsDead == false)
        {
            _targetPosition = _walkingRayChooter.Shoot();

            if (_targetPosition != Vector3.zero)
            {
                IsMoving = true;
            }
        }

        if (Input.GetMouseButtonDown(_leftMouseButton) && IsDead == false)
        {
            _currentTakable = _takingRayShooter.Shoot();

            if (_currentTakable != null)
            _targetPosition = _currentTakable.Take();

            if (_targetPosition != Vector3.zero)
            {
                _isTakingPreparing = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)&&IsJumping==false && IsDead == false)
        {
            IsJumping = true;
        }


        if (_isTakingPreparing)
            PreparationForTake();

        if (IsMoving)
            IsMovingTo();

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsDead = true;
        }
    }


    private void PreparationForTake()
    {
        Vector3 targetPositionXZ = new Vector3(_targetPosition.x, 0, _targetPosition.z);
        Vector3 transformPositionXZ = new Vector3(transform.position.x, 0, transform.position.z);

        if ((Vector3.Distance(targetPositionXZ,transformPositionXZ)) <= _minPositionDifference)
        {
            IsMoving = false;
            _isTakingPreparing = false;
            transform.rotation = _rotator.GetLookrotation(_targetPosition);
            IsTaking = true;
            return;
        }
        IsMoving = true;

    }

    private void IsMovingTo()
    {
        if (Vector3.Magnitude(_targetPosition - transform.position) <= _minPositionDifference)
            IsMoving = false;

        _agent.SetDestination(_targetPosition);
        if (_targetPosition != null)
            _rotator.Update(_targetPosition - transform.position);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth-= damage;
    }
}
