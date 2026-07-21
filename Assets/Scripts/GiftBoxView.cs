using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiftBoxView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GiftBox _giftBox;
    private readonly int _isTakingHash = Animator.StringToHash("IsTaking");

    private void Update()
    {
        if (_giftBox.IsTaking)
            _animator.SetBool(_isTakingHash,true);

    }

    public void DeActiveGiftBox() => _giftBox.DeActiveGiftBox();
}
