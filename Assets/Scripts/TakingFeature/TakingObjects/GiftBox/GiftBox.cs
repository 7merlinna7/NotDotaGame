using UnityEngine;

public class GiftBox : MonoBehaviour, ITakeble
{
    [SerializeField] Transform _takingPositionPoint;

    private Vector3 _takingPosition;
    private bool _isTaking;

    public bool IsTaking {get => _isTaking;}

    public void IsTakingObject () => _isTaking = true;

    public void DeActiveGiftBox() => gameObject.SetActive(false);

    public Vector3 Take()
    {
        return _takingPosition;
    }

    private void Awake()
    {
        _takingPosition = _takingPositionPoint.position;
    }
}
