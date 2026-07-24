using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarChangeUI : MonoBehaviour 
{
    [SerializeField] private Slider _healthSlider;

    private float _maxHealth;
    private float _currentHealth;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _maxHealth = _player.MaxHealth;
    }

    private void Update()
    {
        _currentHealth = _player.CurrentHealth;
        _healthSlider.value = _currentHealth/_maxHealth;
    }
}
