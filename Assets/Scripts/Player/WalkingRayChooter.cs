using UnityEngine;

public class WalkingRayChooter 
{
    public Vector3 Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return ray.GetPoint(hit.distance);
        }
        return Vector3.zero;
    }
}
