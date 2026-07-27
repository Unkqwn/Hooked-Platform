using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Transform cameraTransform;

    [Header("Hook Settings")]
    [SerializeField] private Transform hookOrigin;
    [SerializeField] private float hookRange = 100f;

    [SerializeField] private LayerMask hookableLayer;

    [SerializeField] private float hookSpeed = 10f;
    [SerializeField] private float hookDelay = 0.5f;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    [SerializeField] private float cooldownTime = 1f;
    [SerializeField] private float cooldownTimer = 0f;

    private bool isHooked = false;

    private void Awake()
    {
        if (!playerMove)
        {
            playerMove = GetComponent<PlayerMove>();
        }
        if (!cameraTransform)
        {
            cameraTransform = Camera.main.transform;
        }
        if (!hookOrigin)
        {
            Debug.LogError("Hook origin is not assigned. Assigning the player's transform as the hook origin.");
            hookOrigin = transform;
        }
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Hook action performed.");
            // Implement hook logic here

            StartGrapple();
        }
    }

    private void StartGrapple()
    {
        if (cooldownTimer > 0f)
        {
            Debug.Log("Grapple is on cooldown.");
            return;
        }

        isHooked = true;

        RaycastHit hit;
        if (Physics.Raycast(hookOrigin.position, cameraTransform.forward, out hit, hookRange, hookableLayer))
        {
            grapplePoint = hit.point;
            Debug.Log($"Grapple point set to: {grapplePoint}");
            Invoke(nameof(ExecuteGrapple), hookDelay);
        }
        else
        {
            Debug.Log("No valid grapple point found.");
            grapplePoint = hookOrigin.position + cameraTransform.forward * hookRange;
            Invoke(nameof(StopGrapple), hookDelay);
        }
    }

    private void ExecuteGrapple()
    {
        if (isHooked)
        {
            Vector3 direction = (grapplePoint - transform.position).normalized;
        }
    }

    private void StopGrapple()
    {
        isHooked = false;

        cooldownTimer = cooldownTime;
    }
}