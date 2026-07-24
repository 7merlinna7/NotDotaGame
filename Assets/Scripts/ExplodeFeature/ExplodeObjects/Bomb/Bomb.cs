using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _timeToExplode;
    [SerializeField] private int _explotionDamage;
    [SerializeField] private int _explotionRadius;
    [SerializeField] private bool _isLandBomb;

    private SphereCollider _bombCollider;
    private Vector3 _explodePosition = Vector3.zero;
    private float _explodeAnimationTime =1f;

    public bool IsActive {  get; private set; }
    public bool IsExplode {  get; private set; }

    public void DealExplodeDamage()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, _explotionRadius);
        foreach (Collider target in targets)
        {
            IDamabeble damageble = target.GetComponent<IDamabeble>();
            if (damageble != null)
                damageble.TakeDamage(_explotionDamage);
        }

        Destroy(gameObject);
    }

    private void Awake()
    {
        _bombCollider = GetComponent<SphereCollider>();
        _bombCollider.radius = _explotionRadius*2;
        _bombCollider.isTrigger = true;

        if (_isLandBomb)
            IsActive = true;
    }

    private void Update()
    {
        if ((_isLandBomb == false) && (transform.position.y > _explodePosition.y))
            IsActive = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>() != null && IsActive)
            StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(_timeToExplode - _explodeAnimationTime);
        IsExplode = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _explotionRadius);
    }
}
