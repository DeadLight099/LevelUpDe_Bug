using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float tiltAmount;
    [SerializeField] private bool shouldChangeAngle;
    private float currentAngle;

    public void UpdateAngle()
    {
        currentAngle = Input.GetAxis("Horizontal");
        float targetAngle = currentAngle * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetAngle);
        transform.localRotation = targetRotation;
    }
}
