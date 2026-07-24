using UnityEngine;

public class SmashableRayShooter
{
    private Player _player;
    private ISmashable _smashable;

    public SmashableRayShooter(Player player)
    {
        _player = player;
    }

    public void Shoot()
    {
        Ray ray = new Ray(_player.GetPosition,Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit smashableHit))
        {
            _smashable = smashableHit.collider.GetComponent<ISmashable>();
            if (_smashable != null)
            {
                _smashable.Smash();
            }
        }
    }
}
