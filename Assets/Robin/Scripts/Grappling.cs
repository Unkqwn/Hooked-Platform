using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Transform cameraTransform;

    [Header("Hook Settings")]
    [SerializeField] private float hookRange;

    [SerializeField] private LayerMask hookableLayer;

    [SerializeField] private float hookSpeed;
    [SerializeField] private float hookDelay;

    [Header("Visuals")]
    [SerializeField] private Transform hookOrigin;
    [SerializeField] private LineRenderer ropeRenderer;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    [SerializeField] private float cooldownTime;
    [SerializeField] private float cooldownTimer;

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
            hookOrigin = transform;
        }
        if (!ropeRenderer)
        {
            ropeRenderer = GetComponent<LineRenderer>();
            ropeRenderer.enabled = false;
        }
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (isHooked)
        {
            ropeRenderer.SetPosition(0, hookOrigin.position);
            ropeRenderer.SetPosition(1, grapplePoint);
        }
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Hook action performed.");
            ropeRenderer.SetPosition(0, hookOrigin.position);
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
        
        ropeRenderer.enabled = true;
        ropeRenderer.SetPosition(1, grapplePoint);
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
        ropeRenderer.enabled = false;
        cooldownTimer = cooldownTime;
    }
}