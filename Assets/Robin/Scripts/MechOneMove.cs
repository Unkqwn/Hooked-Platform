using UnityEngine;

public class MechOneMove : EnemyMove
{
    protected override void Chase()
    {
        // Implement chasing behavior specific to MechOne here
        Debug.Log("MechOne is chasing the player!");
        agent.SetDestination(playerTransform.position);
    }
}
