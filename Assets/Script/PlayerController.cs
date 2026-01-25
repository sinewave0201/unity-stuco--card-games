using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   [Header("movement")]
    public float speed = 5f;
    public float jumpForce = 5f;
    private float jumpCooldown = 0.5f;
    private float lastJumpTime;
    public LayerMask groundLayer;

    [Header("rotation")]
    public float mouseSensitivity = 10f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        groundCheck();
    }

    void groundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, 
            transform.TransformDirection(Vector3.down), out hit, 
            0.25f, groundLayer))
            {
                Debug.Log("isGrounded = true");
                Debug.DrawRay(transform.position, 
                            transform.TransformDirection(Vector3.down) * hit.distance, 
                            Color.yellow);
                isGrounded = true;
            }
        else 
        {
            Debug.Log("isGrounded = false");
            isGrounded = false;
        }
    }

    public void jump(InputAction.CallbackContext context)
    {
        Debug.Log("jump is pressed");
        if (isGrounded && Time.time > lastJumpTime + jumpCooldown)
        {
            lastJumpTime = Time.time;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        //rotate movement
        float rotateAmount = lookInput.x * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * rotateAmount);

        //move
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        rb.AddForce(moveDirection * speed);
    }
}
