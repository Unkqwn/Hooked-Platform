using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;

    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float speed = baseSpeed;

        float horizontal = moveInput.x;
        float vertical = moveInput.y;

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
