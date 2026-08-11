using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    Chasing
}

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float chaseDistance = 5f;

    private EnemyState currentState;

    private Transform playerTransform;

    private Transform currentPatrolPoint;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        currentState = EnemyState.Patrolling;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
        }
    }

    private void Patrol()
    {
        if (currentPatrolPoint == null)
        {
            currentPatrolPoint = FindClosestPatrolPoint();
        }

        agent.SetDestination(currentPatrolPoint.position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            int currentIndex = System.Array.IndexOf(patrolPoints, currentPatrolPoint);
            int nextIndex = (currentIndex + 1) % patrolPoints.Length;
            currentPatrolPoint = patrolPoints[nextIndex];
        }
    }

    private void Chase()
    {
        // Implement chasing behavior here
    }

    private void Attack()
    {
        // Implement attack behavior here
    }

    private Transform FindClosestPatrolPoint()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestPoint = null;
        
        foreach (Transform point in patrolPoints)
        {
            float distance = Vector3.Distance(transform.position, point.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }
        return closestPoint;
    }
}
