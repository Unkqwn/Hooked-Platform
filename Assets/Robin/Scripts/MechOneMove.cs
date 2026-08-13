using UnityEngine;

public class MechOneMove : EnemyMove
{
    protected override void Chase()
    {
        // Implement chasing behavior specific to MechOne here
        Debug.Log("MechOne is chasing the player!");
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
        {
            // Implement attack behavior here
            Debug.Log("MechOne is attacking the player!");
        }
        else
        {
            agent.SetDestination(lastKnownPlayerPosition);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetState(EnemyState.Patrolling);
            }
        }
    }
}
