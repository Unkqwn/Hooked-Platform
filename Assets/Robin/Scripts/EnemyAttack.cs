using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected Transform weaponSpawnPoint;
    protected float attackDamage = 10f;
    protected float attackFireRate = 1f;
    protected GameObject weaponPrefab;

    protected float lastAttackTime;

    protected virtual void Start()
    {
        attackDamage = weaponData.Damage;
        attackFireRate = weaponData.FireRate;
        weaponPrefab = weaponData.ProjectilePrefab;

        if (weaponSpawnPoint != null)
        {
            Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation, weaponSpawnPoint);
        }
    }

    public abstract void Attack(Transform target);
}