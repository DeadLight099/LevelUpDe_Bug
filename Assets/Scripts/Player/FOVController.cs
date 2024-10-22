using System.Collections;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    [SerializeField] private float moveFov;
    [SerializeField] private float jumpFov;
    [SerializeField] private float airMovementFov;
    [SerializeField] private bool shouldChangeFov;
    [SerializeField, Range(0f, 3f)] private float transitionSpeed;

    private float defaultFov;
    private float currentFov;

    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
        defaultFov = cam.fieldOfView;
        currentFov = defaultFov;
    }
    private void FixedUpdate()
    {
        if(shouldChangeFov)
            UpdateFOV();
    }
    private void UpdateFOV()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, currentFov, transitionSpeed * Time.deltaTime);
    }
    public void ChangeFOV(bool isMoving, bool isGrounded)
    {
        if (isMoving)
        {
            if (!isGrounded)
            {
                currentFov = airMovementFov;
            }
            else
            {
                currentFov = moveFov;
            }
        }
        else
        {
            if (!isGrounded)
            {
                currentFov = jumpFov;
            }
            else
            {
                currentFov = defaultFov;
            }
        }
    }
}
