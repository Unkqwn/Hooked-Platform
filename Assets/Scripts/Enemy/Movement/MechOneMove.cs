using UnityEngine;

public class MechOneMove : EnemyMove
{
    protected override void Chase()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= minAttackDistance)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            if (enemyAttack != null)
            {
                enemyAttack.Attack(playerTransform);
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(lastKnownPlayerPosition);

            if (Vector3.Distance(transform.position, playerTransform.position) <= maxAttackDistance)
            {
                agent.speed = attackWalkSpeed;
                enemyAttack.Attack(playerTransform);
            }
            else
            {
                agent.speed = chaseSpeed;
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetState(EnemyState.Patrolling);
            }
        }
    }
}
