using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpPower = 5f;

    public Rigidbody rb;

    Vector2 moveInput;

    public Transform playerBody;
    public Transform cam;

    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Run();
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
        LockCursor();
        RotatePlayerCam();
    }

    void Run()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * walkSpeed, rb.velocity.y, moveInput.y * walkSpeed);
        rb.velocity = transform.TransformDirection(playerVelocity);
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

    void RotatePlayerCam()
    {
        playerBody.transform.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
    }
}
