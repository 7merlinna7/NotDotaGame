using UnityEngine;

public class Rotator 
{
    private float _rotationSpeed;
    private Vector3 _currentDirectional;
    private Transform _characterTransform;
    private Quaternion _lookRotation;

    public Rotator(float rotationSpeed,Transform characterTransform)
    {
        _characterTransform = characterTransform;
        _rotationSpeed = rotationSpeed;
    }

    public void Update (Vector3 direction)
    {
        _lookRotation = GetLookrotation(direction);
        float step = _rotationSpeed * Time.deltaTime;
        _characterTransform.rotation = Quaternion.RotateTowards(_characterTransform.rotation, _lookRotation, step);
    }

    public Quaternion GetLookrotation(Vector3 direction)
    {
        return Quaternion.LookRotation(direction.normalized);
    }
}
