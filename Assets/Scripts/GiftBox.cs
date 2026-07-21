using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : MonoBehaviour, ITakeble
{
    [SerializeField] Transform _takingPositionPoint;
    private Vector3 _takingPosition;
    private bool _isTaking;

    public bool IsTaking {  get { return _isTaking; } }

    public void IsTakingObject () => _isTaking = true;

    private void Awake()
    {
        _takingPosition = _takingPositionPoint.position;
    }
    public Vector3 Take()
    {
        return _takingPosition;
    }

    public void DeActiveGiftBox() => gameObject.SetActive(false);
}
