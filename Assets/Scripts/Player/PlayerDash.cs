using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private DashBar dashBar;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    private PlayerMovement playerMovement;
    private Vector3 dashDir;

    private float timer = 3;
    public bool shouldDash = true;
    private bool canDash => shouldDash && (playerMovement._moveDirection.x != 0 || playerMovement._moveDirection.z != 0);

    private void Start()
    {
        if(dashBar != null) dashBar.SetMax(dashCooldown);
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (timer >= dashCooldown && Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            timer = 0;
            StartCoroutine(HandleDash());
        }
        else
        {
            if(characterController.isGrounded)
            {
                timer += Time.deltaTime;
            }
        }
        if(dashBar != null) dashBar.SetValue(timer);
    }
    private IEnumerator HandleDash()
    {
        float startTime = Time.time;
        Debug.Log("Dash!");

        while(Time.time < startTime + dashTime)
        {
            if(dashDir == Vector3.zero)
            {
                dashDir = playerMovement._moveDirection.normalized;
                dashDir.y = 0;
            }

            characterController.Move(dashDir * dashSpeed * Time.deltaTime);


            yield return null;
        }
        dashDir = Vector3.zero;
    }
}
