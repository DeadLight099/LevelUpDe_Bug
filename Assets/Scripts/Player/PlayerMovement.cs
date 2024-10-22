using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool _shouldJump => Input.GetKeyDown(_jumpKey) && _characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _useFootsteps = true;
    [SerializeField] private bool _canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Movement Parametrs")]
    [SerializeField] private float _movementSpeed = 3.0f;

    [Header("Jumping Parametrs")]
    [SerializeField] private GameObject additionalJumpImage;
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 30.0f;
    [SerializeField] private float _jumpSpeed = 5f;
    private float jumpTimer;
    private bool haveJump = false;

    [Header("Attack")]
    [SerializeField] private RobotScript robot;
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private Image interactionPointer;
    [SerializeField] private float rayDistance = 1;
    [SerializeField] private float attackCooldown;
    private float timer;
    private bool canShoot;
    private Color whiteColor;
    private Color redColor;

    [Header("Camera Parametrs")]
    [SerializeField, Range(1, 10)] private float _cameraSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _cameraSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float _upCameraLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float _downCameraLimit = 80.0f;

    [Header("Footsteps Parametrs")]
    [SerializeField] private float _baseStepSpeed = 0.5f;
    [SerializeField] private AudioSource _footstepAudioSource = default;
    [SerializeField] private AudioClip[] _stepClips = default;
    private float _footstepTimer = 0f;
    private float _currentOffset => _baseStepSpeed;

    private Camera _playerCamera;
    private CharacterController _characterController;
    private FOVController fovController;
    private CameraController cameraController;

    public Vector3 _moveDirection;
    private Vector2 _currentInput;

    private float _rotationX = 0;


    private void Awake()
    {
        if (!this.enabled)
            return;

        timer = attackCooldown;

        _playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        fovController = GetComponentInChildren<FOVController>();
        cameraController = GetComponentInChildren<CameraController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetColor();
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            HandleAirSpeed();
            if (_canJump)
            {
                HandleJump();
                HandleJumpUI();
            }

            if (_useFootsteps)
                HandleFootsteps();

            if (_canInteract)
            {
                CountCooldown();
                HandleInteraction();
            }

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        float speed = HandleAirSpeed();

        _currentInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        float moveDirectionY = _moveDirection.y;
        _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x)
            + (transform.TransformDirection(Vector3.right) * _currentInput.y);

        if (_moveDirection.magnitude > 1)
        {
            _moveDirection = _moveDirection.normalized;
        }

        _moveDirection *= speed;

        _moveDirection.y = moveDirectionY;
    }
    

    private void HandleMouseLook()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _cameraSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_upCameraLimit, _downCameraLimit);
        _playerCamera.transform.rotation = Quaternion.Euler(_rotationX, _playerCamera.transform.localRotation.y, _playerCamera.transform.localRotation.z);
        cameraController.UpdateAngle();
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _cameraSpeedX, 0);
    }
    private void ApplyFinalMovements()
    {
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);

        fovController.ChangeFOV(_moveDirection.x != 0 && _moveDirection.z != 0, _characterController.isGrounded);
    }
    private void HandleJumpUI()
    {
        if (haveJump)
        {
            additionalJumpImage.SetActive(true);
        }
        else
        {
            additionalJumpImage.SetActive(false);
        }
    }
    private void HandleJump()
    {
        if (_shouldJump && jumpTimer >= 0.5f)
        {
            _moveDirection.y = _jumpForce;
            SoundManager.instance.Play("Jump");
            haveJump = false;
        }
        else if(Input.GetKeyDown(_jumpKey) && haveJump && jumpTimer >= 0.2f)
        {
            _moveDirection.y = _jumpForce;
            SoundManager.instance.Play("Jump");
            haveJump = false;
        }
        else
        {
            jumpTimer += Time.deltaTime;
        }
    }
    private float HandleAirSpeed()
    {
        if (!_characterController.isGrounded)
        {
            return _movementSpeed + _jumpSpeed;
        }

        return _movementSpeed;
    }

    private void HandleFootsteps()
    {
        if (!_characterController.isGrounded) return;
        if (_currentInput == Vector2.zero) return;

        _footstepTimer -= Time.deltaTime;

        if (_footstepTimer <= 0)
        {
            _footstepAudioSource.PlayOneShot(_stepClips[UnityEngine.Random.Range(0, _stepClips.Length - 1)]);
            _footstepTimer = _currentOffset;
        }
    }

    public void GetJump()
    {
        haveJump = true;
    }
    private void CountCooldown()
    {
        if(timer >= attackCooldown)
        {
            canShoot = true;
            if (Input.GetMouseButton(0))
            {
                timer = 0;
            }
        }
        else
        {
            canShoot = false;
            timer += Time.deltaTime;
        }
    }
    private void HandleInteraction()
    {
        Ray ray = Camera.main.ViewportPointToRay(interactionRayPoint);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.GetComponent<IInteractable>() != null )
            {
                if (Input.GetMouseButton(0) && canShoot)
                {
                    
                    bool isInteract = hit.collider.gameObject.GetComponent<IInteractable>().Interact();
                    if (isInteract) {
                        SoundManager.instance.Play("Laser");
                        StartCoroutine(robot.SendLaser(hit.collider.gameObject.transform));
                        timer = 0;
                    }
                }

                interactionPointer.color = redColor;
            }
            else
            {
                Debug.Log("Smth");
                interactionPointer.color = whiteColor;
            }
        }
        else
        {
            interactionPointer.color = whiteColor;
        }

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);
    }
    private void SetColor()
    {
        whiteColor = Color.white;
        redColor = Color.red;

        whiteColor.a = 1f;
        redColor.a = 1f;
    }

    void OnDrawGizmos()
    {
        Ray ray = Camera.main.ViewportPointToRay(interactionRayPoint);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
