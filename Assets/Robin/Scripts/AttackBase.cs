using UnityEngine;

public abstract class AttackBase : MonoBehaviour
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

    public virtual void Attack(Transform target)
    {
        if (!canAttack || attackOrigin == null)
        {
            return;
        }

        attackOrigin.LookAt(target);
        
        lastAttackTime = 1 / attackFireRate;

        for (int i = 0; i < weaponData.ProjectilesPerShot; i++)
        {
            Vector3 fireDirection = GetRandomSpreadDirection(attackOrigin.forward, weaponData.SpreadAngle);

            Vector3 endPoint = attackOrigin.position + fireDirection * 100f;

            RaycastHit hitInfo;
            if (Physics.Raycast(attackOrigin.position, fireDirection, out hitInfo))
            {
                endPoint = hitInfo.point;
                IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                }
            }
        }
    }

    protected Vector3 GetRandomSpreadDirection(Vector3 forward, float spreadAngle)
    {
        float halfSpread = spreadAngle / 2f;
        float randomYaw = Random.Range(-halfSpread, halfSpread);
        float randomPitch = Random.Range(-halfSpread, halfSpread);

        Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);
        return spreadRotation * forward;
    }
}