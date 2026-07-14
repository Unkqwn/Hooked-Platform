using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 12f;

    private Rigidbody rb;
    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = moveDirection * baseSpeed;
        
        float rate;
        if (moveDirection.sqrMagnitude > 0f)
        {
            rate = acceleration;
        }
        else
        {
            rate = deceleration;
        }

        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetVelocity.x, rate * Time.fixedDeltaTime);
        currentVelocity.z = Mathf.MoveTowards(currentVelocity.z, targetVelocity.z, rate * Time.fixedDeltaTime);

        rb.linearVelocity = currentVelocity;
    }
}
