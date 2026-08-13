using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    Chasing
}

public abstract class EnemyMove : MonoBehaviour
{
    #region Variables
    [Header("NavMesh Settings")]
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected GameObject patrolRoute;

    [SerializeField] protected float baseSpeed = 3.5f;

    [Header("Behavior Settings")]
    [SerializeField] protected float patrolWaitTime = 2f;
    [SerializeField] protected float chaseSpeed = 5f;
    [SerializeField] protected float attackDistance = 1.5f;
    [SerializeField] protected float searchWaitTime = 3f;

    protected EnemyState currentState;

    public Transform playerTransform { get; private set; }
    protected Vector3 lastKnownPlayerPosition;

    protected bool waitingAtLastKnownPosition = false;

    protected Transform[] patrolPoints;
    protected Transform currentPatrolPoint;

    protected bool waitingAtPoint = false;
    #endregion

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
        }
    }

    protected virtual void Patrol()
{
    if (currentPatrolPoint == null)
    {
        currentPatrolPoint = FindClosestPatrolPoint();
    }

    agent.SetDestination(currentPatrolPoint.position);

    if (!waitingAtPoint && !agent.pathPending && agent.remainingDistance < 0.5f)
    {
        waitingAtPoint = true;
        agent.isStopped = true;
        Invoke(nameof(GetNextPatrolPoint), patrolWaitTime);
    }
}

protected void GetNextPatrolPoint()
{
    int currentIndex = System.Array.IndexOf(patrolPoints, currentPatrolPoint);
    int nextIndex = (currentIndex + 1) % patrolPoints.Length;
    currentPatrolPoint = patrolPoints[nextIndex];
    agent.isStopped = false;
    waitingAtPoint = false;
}

    protected abstract void Chase();

    protected void SearchLastKnownPosition()
    {
        agent.SetDestination(lastKnownPlayerPosition);

        if (!waitingAtLastKnownPosition && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waitingAtLastKnownPosition = true;
            agent.isStopped = true;
            Invoke(nameof(GiveUpSearch), searchWaitTime);
        }
    }

    protected void GiveUpSearch()
    {
        waitingAtLastKnownPosition = false;
        SetState(EnemyState.Patrolling);
    }

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

    public void SetState(EnemyState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        agent.isStopped = false;

        switch (newState)
        {
            case EnemyState.Patrolling:
                agent.speed = baseSpeed;
                break;

            case EnemyState.Chasing:
                CancelInvoke(nameof(GetNextPatrolPoint));
                waitingAtPoint = false;
                agent.speed = chaseSpeed;
                break;
        }
    }

    public void SetLastKnownPlayerPosition(Vector3 position)
    {
        lastKnownPlayerPosition = position;

        if (waitingAtLastKnownPosition)
        {
            waitingAtLastKnownPosition = false;
            CancelInvoke(nameof(GiveUpSearch));
            agent.isStopped = false;
        }
    }
}