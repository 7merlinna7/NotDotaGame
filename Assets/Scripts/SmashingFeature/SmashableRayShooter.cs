using UnityEngine;

public class SmashableRayShooter
{
    private Player _player;
    private LayerMask _smashingLayerMask;
    private ISmashable _smashable;

    public SmashableRayShooter(Player player, LayerMask smashingLayerMask)
    {
        _player = player;
        _smashingLayerMask = smashingLayerMask;
    }

    public void Shoot()
    {
        Ray ray = new Ray(_player.GetPosition,Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit smashableHit,Mathf.Infinity,_smashingLayerMask))
        {
            _smashable = smashableHit.collider.GetComponent<ISmashable>();
            if (_smashable != null)
            {
                _smashable.Smash();
            }
        }
    }
}
