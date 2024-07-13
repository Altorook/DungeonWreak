using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] 
    private Transform playerTransform;
    [SerializeField] 
    private Camera playerCamera;
    [SerializeField] 
    private Rigidbody playerRigidbody;
    
    [SerializeField] 
    private float mouseSensitivity = 2f;
    [SerializeField]
    private float maxLookAngle = 85f;

    [SerializeField]
    private float walkSpeed = 7f;
    [SerializeField] 
    private float sprintSpeed = 12f;
    
    [SerializeField] 
    private float maxStamina = 1000f;
    [SerializeField]
    private float staminaDrainRate = 30f;
    [SerializeField] 
    private float staminaRecoveryRate = 25f;

    
    private bool isSprinting = false;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float rotationSpeed;
    public float stamina = 1000f;

    private Vector3 _movementDirection;
    private void OnEnable()
    {
        inputManager.onMove += OnMove;
        inputManager.onSprint += OnSprint;
        inputManager.onLook += OnLook;
    }

    private void OnDisable()
    {
        inputManager.onMove -= OnMove;
        inputManager.onSprint -= OnSprint;
        inputManager.onLook -= OnLook;
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void Update()
    {
        HandleSprint();
    }
    private void OnMove(Vector2 inputValue)
    {
        _movementDirection = new Vector3(inputValue.x, 0, inputValue.y);

        _movementDirection = Quaternion.Euler(0, playerTransform.eulerAngles.y, 0) * _movementDirection;
    }
    private void HandleMovement()
    {
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 velocity = _movementDirection * currentSpeed;

        playerRigidbody.velocity = new Vector3(velocity.x, playerRigidbody.velocity.y, velocity.z);
    }
 
    private void OnLook(Vector2 lookValue)
    {
        float mouseX = lookValue.x * mouseSensitivity * Time.deltaTime;
        float mouseY = Mathf.Clamp(lookValue.y * mouseSensitivity * Time.deltaTime, -maxLookAngle, maxLookAngle);

      
        xRotation += mouseX;
        yRotation -= mouseY; 

        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        
        transform.localRotation = Quaternion.Euler(0f, xRotation, 0f); 
        playerCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f); 

    }
    private void OnSprint(bool sprintValue)
    {
        if (sprintValue  && stamina > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        if (isSprinting)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                isSprinting = false;
            }
        }
        else if (stamina < maxStamina)
        {
            stamina += staminaRecoveryRate * Time.deltaTime;
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }
    private void HandleSprint()
    {
        if (isSprinting)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                isSprinting = false;
            }
        }
        else if (stamina < maxStamina)
        {
            stamina += staminaRecoveryRate * Time.deltaTime;
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }





}
