using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4.0f;
    public float sprintSpeed = 7.5f;
    public float acceleration = 10f;       
    public float deceleration = 12f;
    public float gravity = -20f;
    public float jumpHeight = 1.6f;
    [Tooltip("Max angle (degrees) before slide off slopes when using CharacterController")]
    public float slopeLimit = 45f;

    [Header("Mouse Look")]
    public Transform cameraTransform;      
    public float mouseSensitivity = 1.8f;
    public float mouseSmoothing = 0.05f;
    public float minPitch = -85f;
    public float maxPitch = 85f;

    [Header("Options")]
    public bool lockCursor = true;
    public bool allowSprint = true;

    
    CharacterController cc;
    Vector3 velocity;            
    Vector2 currentInput;       
    Vector2 inputVelocity;      
    Vector2 currentMouseDelta;
    Vector2 mouseSmoothVelocity;
    float yaw;                  
    float pitch;                

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cc.slopeLimit = slopeLimit;
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        
        yaw = transform.eulerAngles.y;
        if (cameraTransform != null)
            pitch = cameraTransform.localEulerAngles.x;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleMouseLook()
    {
        if (cameraTransform == null) return;

        
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        
        float smoothedX = Mathf.SmoothDamp(currentMouseDelta.x, mouseDelta.x * mouseSensitivity, ref mouseSmoothVelocity.x, mouseSmoothing);
        float smoothedY = Mathf.SmoothDamp(currentMouseDelta.y, mouseDelta.y * mouseSensitivity, ref mouseSmoothVelocity.y, mouseSmoothing);
        currentMouseDelta = new Vector2(smoothedX, smoothedY);

        yaw += currentMouseDelta.x;
        pitch -= currentMouseDelta.y; 
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void HandleMovement()
    {
       
        Vector2 targetInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetInput = Vector2.ClampMagnitude(targetInput, 1f);

        
        float smoothTime = (targetInput.magnitude > currentInput.magnitude) ? (1f / acceleration) : (1f / deceleration);
        currentInput.x = Mathf.SmoothDamp(currentInput.x, targetInput.x, ref inputVelocity.x, smoothTime);
        currentInput.y = Mathf.SmoothDamp(currentInput.y, targetInput.y, ref inputVelocity.y, smoothTime);

       
        bool sprintPressed = allowSprint && Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = sprintPressed ? sprintSpeed : walkSpeed;
        
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 desiredMove = (forward * currentInput.y + right * currentInput.x);
        if (desiredMove.sqrMagnitude > 1f) desiredMove.Normalize();

        
        Vector3 horizontal = desiredMove * targetSpeed;

        
        if (cc.isGrounded)
        {
          
            if (velocity.y < 0f) velocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                
                velocity.y = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight);
            }
        }
        else
        {
            
            velocity.y += gravity * Time.deltaTime;
        }

        
        Vector3 move = horizontal + new Vector3(0f, velocity.y, 0f);
        cc.Move(move * Time.deltaTime);
    }

    
    void OnDrawGizmosSelected()
    {
        if (cameraTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(cameraTransform.position, 0.05f);
            Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, cameraTransform.position);
        }
    }
}