using UnityEngine;

public class MechOneMove : EnemyMove
{
    protected override void Chase()
    {
       Vector3 aimDirection = (playerTransform.position - enemyAttack.AttackOrigin.position).normalized;
        if (Vector3.Distance(transform.position, playerTransform.position) <= minAttackDistance)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            if (enemyAttack != null)
            {
                enemyAttack.Attack(aimDirection);
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(lastKnownPlayerPosition);

            if (Vector3.Distance(transform.position, playerTransform.position) <= maxAttackDistance)
            {
                agent.speed = attackWalkSpeed;
                enemyAttack.Attack(aimDirection);
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
