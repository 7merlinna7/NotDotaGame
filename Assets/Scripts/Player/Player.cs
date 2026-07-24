using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour,IDamabeble
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxHealth;

    private Rotator _rotator;
    private NavMeshAgent _agent;

    private float _minPositionDifference = 0.5f;
    private SmashableRayShooter _smashableRayShooter;
    private PlayerInput _playerInput;

    public bool IsMoving { get; set; }
    public bool IsJumping { get; set; }
    public bool IsDead { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get => _maxHealth; }
    public Vector3 GetPosition { get => transform.position; }

    private void Awake()
    {
        _rotator = new Rotator(_rotationSpeed,transform);

        _smashableRayShooter = new SmashableRayShooter(this);

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _playerInput = new PlayerInput(this);
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

    public void StopJumping()
    {
        _smashableRayShooter.Shoot();
        IsJumping = false;
    }

    public void IsMovingTo(Vector3 target)
    {
        if (Vector3.Magnitude(target - transform.position) <= _minPositionDifference)
            IsMoving = false;

        _agent.SetDestination(target);
        if (target != null)
            _rotator.Update(target - transform.position);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth-= damage;
    }

    public bool IsWounded()
    {
        if(CurrentHealth / MaxHealth <= 0.5)
            return true;

        return false;
    }
}
