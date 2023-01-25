using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public EcsEntity entity;
    public float meleeAttackDistance;
    public float meleeAttackInterval;
    public float startHealth;
    public float startArmor;
    public float damage;
}