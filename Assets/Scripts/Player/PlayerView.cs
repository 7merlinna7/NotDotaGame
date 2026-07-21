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
    private int _isTakingLayerIndex;
    private float _isTakingWeight = 0.4f;

    private void Awake()
    {
        _isTakingLayerIndex = _animator.GetLayerIndex("Taking Layer");
    }
    private void Update()
    {
        if (_player.IsMoving)
            _animator.SetBool(_isWalkingHash, true);

        if (_player.IsMoving == false)
            _animator.SetBool(_isWalkingHash, false);

        if (_player.IsTaking)
        {
            _animator.SetLayerWeight(_isTakingLayerIndex, _isTakingWeight);
            _animator.SetBool(_isTakingHash, true);
        }

        if (_player.IsTaking == false)
            _animator.SetLayerWeight(_isTakingLayerIndex, 0);
    }
    public void StopTaking()
    {
        _player.CurrentTakable.IsTakingObject();
        _player.StopTaking();
        _animator.SetBool(_isTakingHash, false);
    }
}
