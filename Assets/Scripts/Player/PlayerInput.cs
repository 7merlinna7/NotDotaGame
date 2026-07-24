using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput 
{
    private const int _leftMouseButton = 0;
    private const int _rightMouseButton = 1;

    private Player _player;

    public PlayerInput(Player player)
    {
        _player = player;
    }

    //public void Start()

    public void Update ()
    {
        if (_player.IsDead == false)
        {
            
        }
    }
}
