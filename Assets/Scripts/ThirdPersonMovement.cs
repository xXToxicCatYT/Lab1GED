using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float jumpPower = 5f;

    [Header("Ground Check")]
    public float groundCheckRadius = 0.3f;      // Sphere radius for ground detection
    public float groundCheckOffset = 1.1f;      // How far below the player’s center to check
    public LayerMask groundMask;                // Set this to your ground layer(s)

    private Vector2 moveInput;
    private Rigidbody rb;
    private bool isGrounded;

    [Header("References")]
    public Transform cam;                       // Camera for movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;               // Prevent unwanted tipping
    }

    void Update()
    {
        GroundCheck();

        // Jump input only fires if we're grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        LockCursor();
    }

    void FixedUpdate()
    {
        Run();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Run()
    {
        // Calculate movement relative to camera direction
        Vector3 forward = cam != null ? cam.forward : Vector3.forward;
        Vector3 right = cam != null ? cam.right : Vector3.right;

        forward.y = 0f; // Ignore camera pitch
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;
        Vector3 velocity = moveDirection * walkSpeed;
        velocity.y = rb.velocity.y; // Preserve vertical velocity so jump is not cancelled
        rb.velocity = velocity;
    }

    private void Jump()
    {
        // Reset vertical velocity before applying jump to keep it consistent
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        // Check a point slightly below the player’s position
        Vector3 checkPos = transform.position + Vector3.down * groundCheckOffset;
        isGrounded = Physics.CheckSphere(checkPos, groundCheckRadius, groundMask);

    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
