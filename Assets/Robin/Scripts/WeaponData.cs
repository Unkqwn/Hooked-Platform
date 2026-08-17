using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName = "Default Weapon";
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField, Range(0f, 50f)] private float spreadAngle = 0f;
    [SerializeField] private GameObject projectilePrefab;

    public string WeaponName => weaponName;
    public float Damage => damage;
    public float FireRate => fireRate;
    public float SpreadAngle => spreadAngle;
    public GameObject ProjectilePrefab => projectilePrefab;
}
