using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;

    public float Health => health;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health += amount;
    }

    public void Die()
    {
        // Implementation for player death
    }
}
