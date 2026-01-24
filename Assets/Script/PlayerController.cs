using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   [SerializeField]
    public float speed = 5f;

    [SerializeField]
    private Rigidbody rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
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
