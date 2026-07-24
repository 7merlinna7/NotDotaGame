using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    private readonly int _isWalkingHash = Animator.StringToHash("IsWalking");
    private readonly int _isJumpingHash = Animator.StringToHash("IsJumping");
    private readonly int _isTakingHash = Animator.StringToHash("IsTaking");
    private readonly int _isDeadHash = Animator.StringToHash("IsDead");
    private int _isTakingLayerIndex;
    private int _isWoundedLayerIndex;
    private float _isTakingWeight = 0.4f;
    private float _isWoundedWeight = 0.6f;

    private void Awake()
    {
        _isTakingLayerIndex = _animator.GetLayerIndex("Taking Layer");
        _isWoundedLayerIndex = _animator.GetLayerIndex("Wounded Layer");
    }
    private void Update()
    {
        if (_player.IsMoving)
            _animator.SetBool(_isWalkingHash, true);
        else
            _animator.SetBool(_isWalkingHash, false);

        if (_player.IsTaking)
        {
            _animator.SetLayerWeight(_isTakingLayerIndex, _isTakingWeight);
            _animator.SetBool(_isTakingHash, true);
        }
        else
        {
            _animator.SetLayerWeight(_isTakingLayerIndex, 0);
        }
            
        if (_player.CurrentHealth/_player.MaxHealth <= 0.5)
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

    public void StopTaking()
    {
        _player.CurrentTakable.IsTakingObject();
        _player.StopTaking();
        _animator.SetBool(_isTakingHash, false);
    }
}
