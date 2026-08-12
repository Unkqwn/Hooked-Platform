using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    Chasing,
    Attacking
}

public abstract class EnemyMove : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected GameObject patrolRoute;

    [SerializeField] protected float chaseDistance = 5f;
    [SerializeField] protected float attackDistance = 1.5f;

    [SerializeField] protected float baseSpeed = 3.5f;

    protected EnemyState currentState;

    protected Transform playerTransform;

    protected Transform[] patrolPoints;
    protected Transform currentPatrolPoint;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        agent.speed = baseSpeed;

        currentState = EnemyState.Patrolling;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (patrolRoute != null)
        {
            patrolPoints = new Transform[patrolRoute.transform.childCount];
            for (int i = 0; i < patrolRoute.transform.childCount; i++)
            {
                patrolPoints[i] = patrolRoute.transform.GetChild(i);
            }
        }
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
            case EnemyState.Attacking:
                Attack();
                break;
        }
    }

    protected virtual void Patrol()
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

    protected abstract void Chase();

    protected abstract void Attack();

    protected Transform FindClosestPatrolPoint()
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
