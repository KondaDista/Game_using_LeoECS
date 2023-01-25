using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticData", menuName = "StaticData")]
public class StaticData : ScriptableObject
{
    public GameObject playerPrefab;
    public float playerHealth;
    public float playerArmor;
    public float playerSpeed;
}
