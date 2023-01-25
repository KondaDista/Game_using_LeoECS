using UnityEngine;
using UnityEngine.AI;

public struct Enemy
{
    public GameObject enemyGO;
    public NavMeshAgent navMeshAgent;
    public Transform transform;
    public float meleeAttackDistance;
    public float meleeAttackInterval;
    public float damage;
}