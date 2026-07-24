using UnityEngine;

public class BombView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Bomb _bomb;
    [SerializeField] private ParticleSystem _explodeEffect;

    private readonly int _isActiveHash = Animator.StringToHash("IsActive");
    private readonly int _isExplodeHash = Animator.StringToHash("IsExplode");

    public void StartExplodeEffect()
    {
        Instantiate(_explodeEffect, transform.position, Quaternion.identity);
        _bomb.DealExplodeDamage();
    }

    private void Update()
    {
        if (_bomb.IsActive)
            _animator.SetBool(_isActiveHash,true);

        if (_bomb.IsExplode)
            _animator.SetBool(_isExplodeHash, true);
    }
}
