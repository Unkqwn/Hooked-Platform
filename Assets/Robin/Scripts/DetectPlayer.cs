using UnityEngine;
using System;

[Flags]
public enum DetectType
{
    Vision = 1 << 0,
    Sensor = 1 << 1,
    Hearing = 1 << 2
}

public class DetectPlayer : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private DetectType detectType;

    [Header("Vision Settings")]
    [SerializeField] private float visionDistance = 10f;
    [SerializeField, Range(45, 180)] private float visionAngle = 45f;

    [Header("Sensor Settings")]
    [SerializeField] private float sensorRadius = 5f;

    [Header("Hearing Settings")]
    [SerializeField] private float hearingDistance = 15f;

    private EnemyMove enemyMove;

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Update()
    {
        bool playerDetected = false;

        if (((detectType & DetectType.Vision) != 0))
        {
            playerDetected |= DetectPlayerByVision();
        }

        if (((detectType & DetectType.Sensor) != 0))
        {
            playerDetected |= DetectPlayerBySensor();
        }

        if (((detectType & DetectType.Hearing) != 0))
        {
            playerDetected |= DetectPlayerByHearing();
        }

        if (playerDetected)
        {
            enemyMove.SetLastKnownPlayerPosition(enemyMove.playerTransform.position);
            enemyMove.SetState(EnemyState.Chasing);
        }
    }

    private bool DetectPlayerByVision()
    {
        return false;
    }

    private bool DetectPlayerBySensor()
    {
        if (enemyMove == null) return false;

        if (Vector3.Distance(transform.position, enemyMove.playerTransform.position) <= sensorRadius)
        {
            return true;
        }
        return false;
    }

    private bool DetectPlayerByHearing()
    {
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if ((detectType & DetectType.Sensor) != 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sensorRadius);
        }

        if ((detectType & DetectType.Hearing) != 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, hearingDistance);
        }

        if ((detectType & DetectType.Vision) != 0)
        {
            DrawVisionCone();
        }
    }

    private void DrawVisionCone()
    {
        Gizmos.color = Color.cyan;

        Vector3 forward = transform.forward * visionDistance;
        Vector3 leftBoundary = Quaternion.AngleAxis(-visionAngle / 2f, transform.up) * forward;
        Vector3 rightBoundary = Quaternion.AngleAxis(visionAngle / 2f, transform.up) * forward;

        // The two edge lines of the cone
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // Arc connecting the edges, approximated with line segments
        Vector3 previousPoint = transform.position + leftBoundary;
        int segments = 20;
        for (int i = 1; i <= segments; i++)
        {
            float angle = -visionAngle / 2f + (visionAngle * i / segments);
            Vector3 nextPoint = transform.position + Quaternion.AngleAxis(angle, transform.up) * forward;
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }
}