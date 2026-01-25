using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   [SerializeField]
    public float speed = 5f;
    public float jumpForce = 5f;
    private float jumpCooldown = 0.5f;
    private float lastJumpTime;
    public LayerMask groundLayer;

    [SerializeField]
    private Rigidbody rb;
    private Vector2 input;

    [SerializeField]
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
        input = context.ReadValue<Vector2>();
        Debug.Log("Move: " + input);
    }

    void FixedUpdate()
    {
        Vector3 Force = new Vector3(input.x, 0, input.y);
        Force *= speed;
        rb.AddForce(Force);
    }
}
