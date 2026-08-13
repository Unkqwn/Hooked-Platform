using UnityEngine;
using System;

[Flags]
public enum DetectType
{
    Vision = 1 << 0,
    Sound = 1 << 1
}

public class DetectPlayer : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private DetectType detectType;

    [Header("Vision Settings")]
    [SerializeField] private float visionDistance = 10f;
    [SerializeField] private float visionAngle = 45f;

    [Header("Hearing Settings")]
    [SerializeField] private float hearingRadius = 5f;

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

        if (((detectType & DetectType.Sound) != 0))
        {
            playerDetected |= DetectPlayerBySound();
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

    private bool DetectPlayerBySound()
    {
        if (enemyMove == null) return false;

        if (Vector3.Distance(transform.position, enemyMove.PlayerTransform.position) <= hearingRadius)
        {
            return true;
        }
        return false;
    }
}