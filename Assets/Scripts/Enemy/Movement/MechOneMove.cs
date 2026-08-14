using UnityEngine;

public class MechOneMove : EnemyMove
{
    protected override void Chase()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            Debug.Log("MechOne is attacking the player!");
        }
        else
        {
            agent.speed = chaseSpeed;
            
            agent.isStopped = false;
            agent.SetDestination(lastKnownPlayerPosition);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetState(EnemyState.Patrolling);
            }
        }
    }
}
