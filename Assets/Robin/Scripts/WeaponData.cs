using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName = "Default Weapon";
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject projectilePrefab;

    public string WeaponName => weaponName;
    public float Damage => damage;
    public float FireRate => fireRate;
    public GameObject ProjectilePrefab => projectilePrefab;
}
