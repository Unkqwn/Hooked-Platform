using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the player object.");
        }

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 cameraForward = Vector3.forward;
        Vector3 cameraRight = Vector3.right;
        Vector3 moveDirection = Vector3.zero;

        if (cameraTransform != null)
        {
            float cameraYaw = cameraTransform.eulerAngles.y;
            Quaternion yawRotation = Quaternion.Euler(0f, cameraYaw, 0f);
            cameraForward = yawRotation * Vector3.forward;
            cameraRight = yawRotation * Vector3.right;
            moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;
        }
        else
        {
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        }

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
