using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected Transform weaponSpawnPoint;
    protected Transform attackOrigin;
    protected float attackDamage = 10f;
    protected float attackFireRate = 1f;
    protected GameObject weaponPrefab;

    protected float lastAttackTime;

    protected bool canAttack = true;

    protected virtual void Start()
    {
        attackDamage = weaponData.Damage;
        attackFireRate = weaponData.FireRate;
        weaponPrefab = weaponData.ProjectilePrefab;

        if (weaponSpawnPoint != null)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation, weaponSpawnPoint);

            if (weaponInstance == null)
            {
                Debug.LogWarning($"Failed to instantiate weapon prefab '{weaponPrefab.name}' for {gameObject.name}.");
                return;
            }
            attackOrigin = FindDeepChild(weaponInstance.transform, "Tip");

            if (attackOrigin == null)
            {
                Debug.LogWarning($"No 'Tip' found on weapon prefab '{weaponPrefab.name}' for {gameObject.name}.");
                attackOrigin = weaponInstance.transform; // Fallback to the weapon's root if "Tip" is not found
            }
        }
    }

    protected virtual void Update()
    {
        if (lastAttackTime > 0)
        {
            lastAttackTime -= Time.deltaTime;
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }

    protected Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }

    public abstract void Attack(Transform target);
}