using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSettings : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnTransform;
    public float bulletSpeed;
    public float bulletRadius;
    public int weaponDamage;
    public int currentInMagazine;
    public int maxInMagazine;
    public int totalAmmo;
}
