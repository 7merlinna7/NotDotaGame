using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private const int _leftMouseButton = 0;
    private const int _rightMouseButton = 1;

    [SerializeField] private float _rotationSpeed;

    private WalkingRayChooter _walkingRayChooter;
    private TakingRayShooter _takingRayShooter;
    private Rotator _rotator;
    private NavMeshAgent _agent;

    private Vector3 _targetPosition;
    private float _minPositionDifference = 0.5f;
    private float _minTakePositionDifference = 0.1f;

    private bool _isMoving;
    private bool _isTakingPreparing;
    private bool _isTaking;
    private ITakeble _currentTakable;

    public bool IsMoving { get => _isMoving; }
    public bool IsTaking { get => _isTaking; }
    public ITakeble CurrentTakable { get => _currentTakable; }

    public void StopTaking()
    {
        _isTaking = false;
        _currentTakable = null;
    }

    private void Awake()
    {
        _walkingRayChooter = new WalkingRayChooter();
        _takingRayShooter = new TakingRayShooter(this);
        
        _rotator = new Rotator(_rotationSpeed,transform);
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _isMoving = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_rightMouseButton))
        {
            _targetPosition = _walkingRayChooter.Shoot();

            if (_targetPosition != Vector3.zero)
            {
                _isMoving = true;
            }
        }

        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            _currentTakable = _takingRayShooter.Shoot();
            _targetPosition = _currentTakable.Take();

            Debug.Log(_targetPosition);

            if (_targetPosition != Vector3.zero)
            {
                _isTakingPreparing = true;
            }
        }

        if (_isTakingPreparing)
            PreparationForTake();

        if (_isMoving)
            IsMovingTo();
    }


    private void PreparationForTake()
    {
        if (Vector3.Magnitude(_targetPosition - transform.position) <= _minTakePositionDifference)
        {
            _isMoving = false;
            _isTakingPreparing = false;
            transform.rotation = _rotator.GetLookrotation(_targetPosition);
            _isTaking = true;
            return;
        }

        _isMoving = true;

    }

    private void IsMovingTo()
    {
        if (Vector3.Magnitude(_targetPosition - transform.position) <= _minPositionDifference)
            _isMoving = false;

        _agent.SetDestination(_targetPosition);
        if (_targetPosition != null)
            _rotator.Update(_targetPosition - transform.position);
    }
}
