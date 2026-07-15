using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float ySensitivity;
    [SerializeField, Range(0f, 100f)] private float xSensitivity;
    [SerializeField] private InputActionReference lookAction; // bind to your Look action

    private Transform cameraTransform;
    private float xRotation;
    private float yRotation;

    private void OnEnable()  => lookAction.action.Enable();
    private void OnDisable() => lookAction.action.Disable();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        HandleCameraRotation(lookInput);
    }

    private void HandleCameraRotation(Vector2 lookInput)
    {
        xRotation -= lookInput.y * ySensitivity * Time.deltaTime;
        yRotation += lookInput.x * xSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}