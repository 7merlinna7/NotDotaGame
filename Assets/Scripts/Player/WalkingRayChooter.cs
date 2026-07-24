using UnityEngine;

public class WalkingRayChooter 
{
    private LayerMask _layerMask;

    public WalkingRayChooter(LayerMask layerMask)
    {
        _layerMask = layerMask;
    }

    public Vector3 Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity,_layerMask.value))
        {
            return ray.GetPoint(hit.distance);
        }
        return Vector3.zero;
    }
}
