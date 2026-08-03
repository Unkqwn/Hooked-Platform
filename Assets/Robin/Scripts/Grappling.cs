using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Transform cameraTransform;

    [Header("Hook Settings")]
    [SerializeField] private LayerMask hookableLayer;

    [SerializeField] private float hookSpeed;
    [SerializeField] private float hookDelay;

    [Header("Cable Settings")]
    [SerializeField] private float cableMaxLength;
    [SerializeField] private float maxDistanceModifier = 0.8f;
    [SerializeField] private float minDistanceModifier = 0.25f;
    private SpringJoint springJoint;


    [Header("Visuals")]
    [SerializeField] private Transform hookOrigin;
    [SerializeField] private LineRenderer ropeRenderer;

    private Vector3 hookPoint;

    [Header("Cooldown")]
    [SerializeField] private float cooldownTime;
    [SerializeField] private float cooldownTimer;

    private bool isHooked = false;
    #endregion

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
            ropeRenderer.SetPosition(1, hookPoint);
        }
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isHooked)
            {
                StopHook();
                return;
            }
            ropeRenderer.SetPosition(0, hookOrigin.position);
            ShootHook();
        }
    }

    private void ShootHook()
    {
        if (cooldownTimer > 0f)
        {
            return;
        }

        isHooked = true;

        RaycastHit hit;
        if (Physics.Raycast(hookOrigin.position, cameraTransform.forward, out hit, cableMaxLength, hookableLayer))
        {
            hookPoint = hit.point;
            Invoke(nameof(StartSwing), hookDelay);
        }
        else
        {
            hookPoint = hookOrigin.position + cameraTransform.forward * cableMaxLength;
            Invoke(nameof(StopHook), hookDelay);
        }
        
        playerMove.enabled = false;

        ropeRenderer.enabled = true;
        ropeRenderer.SetPosition(1, hookPoint);
    }

    private void StopHook()
    {
        isHooked = false;
        ropeRenderer.enabled = false;
        cooldownTimer = cooldownTime;
        playerMove.enabled = true;
        StopSwing();
    }

    private void StartSwing()
    {
        springJoint = playerMove.gameObject.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = hookPoint;

        float distanceFromPoint = Vector3.Distance(playerMove.transform.position, hookPoint);
        springJoint.maxDistance = distanceFromPoint * maxDistanceModifier;
        springJoint.minDistance = distanceFromPoint * minDistanceModifier;

        springJoint.spring = 4.5f;
        springJoint.damper = 7f;
        springJoint.massScale = 4.5f;
    }

    private void StopSwing()
    {
        if (springJoint != null)
        {
            Destroy(springJoint);
        }
    }
}