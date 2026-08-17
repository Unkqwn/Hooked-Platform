using UnityEngine;

public class MechOneAttack : EnemyAttack
{
    public override void Attack(Transform target)
    {
        if (!canAttack || attackOrigin == null)
        {
            return;
        }

        attackOrigin.LookAt(target);
        
        lastAttackTime = 1 / attackFireRate;

        Vector3 fireDirection = GetRandomSpreadDirection(attackOrigin.forward, weaponData.SpreadAngle);

        RaycastHit hitInfo;
        if (Physics.Raycast(attackOrigin.position, fireDirection, out hitInfo))
        {
            IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }
}