using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private static string HorizontalAxdisName = "Horizontal";
    private static string VerticalAxisName = "Vertical";
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw(HorizontalAxdisName), 0, Input.GetAxisRaw(VerticalAxisName));
        Vector3 normalizedInput = input.normalized;
        ProcessMoveTo(normalizedInput);
    }
    public void ProcessMoveTo(Vector3 direction) => _characterController.Move(direction * _moveSpeed * Time.deltaTime);
}
