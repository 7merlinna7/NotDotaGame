using UnityEngine;
public class TakingRayShooter 
{
    private ITakeble _takeble;
    private Player _player;

    public TakingRayShooter(Player player)
    {
        _player = player;
    }

    public ITakeble Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit takableHit))
        {
            _takeble = takableHit.collider.GetComponent<ITakeble>();
            if (_takeble != null)
            {
                return _takeble;
            }
        }
        return null;
    }
}
