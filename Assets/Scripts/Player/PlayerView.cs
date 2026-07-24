using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    private readonly int _isWalkingHash = Animator.StringToHash("IsWalking");
    private readonly int _isJumpingHash = Animator.StringToHash("IsJumping");
    private readonly int _isDeadHash = Animator.StringToHash("IsDead");

    private int _isWoundedLayerIndex;
    private float _isWoundedWeight = 0.6f;

    private void Awake()
    {
        _isWoundedLayerIndex = _animator.GetLayerIndex("Wounded Layer");
    }
    private void Update()
    {
        if (_player.IsMoving)
            _animator.SetBool(_isWalkingHash, true);
        else
            _animator.SetBool(_isWalkingHash, false);
            
        if (_player.IsWounded())
            _animator.SetLayerWeight(_isWoundedLayerIndex, _isWoundedWeight);
        else _animator.SetLayerWeight(_isWoundedLayerIndex, 0);

        if (_player.IsJumping)
            _animator.SetBool(_isJumpingHash, true);
        else
            _animator.SetBool(_isJumpingHash, false);

        if (_player.IsDead)
            _animator.SetBool(_isDeadHash, true);
    }

    public void StopJumping() => _player.StopJumping();
}
