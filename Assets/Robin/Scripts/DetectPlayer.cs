using UnityEngine;
using System;

[Flags]
public enum DetectType
{
    Vision = 1 << 0,
    Sensor = 1 << 1
}

public class DetectPlayer : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private DetectType detectType;

    [Header("Vision Settings")]
    [SerializeField] private float visionDistance = 10f;
    [SerializeField] private float visionAngle = 45f;

    [Header("Sensor Settings")]
    [SerializeField] private float sensorRadius = 5f;

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

        if (playerDetected)
        {
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

        if (Vector3.Distance(transform.position, enemyMove.PlayerTransform.position) <= sensorRadius)
        {
            return true;
        }
        return false;
    }
}