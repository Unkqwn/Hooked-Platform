using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName = "Default Weapon";
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField, Range(0f, 1f)] private float spreadAngle = 0f;
    [SerializeField, Range(1, 10)] private int projectilesPerShot = 1;
    [SerializeField] private GameObject projectilePrefab;

    public string WeaponName => weaponName;
    public float Damage => damage;
    public float FireRate => fireRate;
    public float SpreadAngle => spreadAngle;
    public int ProjectilesPerShot => projectilesPerShot;
    public GameObject ProjectilePrefab => projectilePrefab;
}
