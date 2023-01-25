using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class EnemyInitSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private EnemyData enemyData;
    private SceneData sceneData;

    public float timeToSpawn = 1;

    public void Init()
    {
        EcsEntity enemyEntity = ecsWorld.NewEntity();

        ref var enemy = ref enemyEntity.Get<Enemy>();
        ref var health = ref enemyEntity.Get<Health>();
        enemyEntity.Get<Idle>();

        GameObject enemyReady = SpawnEnemy();
        var enemyView = enemyReady.GetComponent<EnemyView>();

        health.health = enemyView.startHealth;
        health.armor = enemyView.startArmor;
        enemy.enemyGO = enemyReady;
        enemy.damage = enemyView.damage;
        enemy.meleeAttackDistance = enemyView.meleeAttackDistance;
        enemy.navMeshAgent = enemyView.navMeshAgent;
        enemy.transform = enemyView.transform;
        enemy.meleeAttackInterval = enemyView.meleeAttackInterval;

        enemyView.entity = enemyEntity;
    }

    public void Run()
    {
        if (sceneData.totalEnemy < 100 && Time.time >= timeToSpawn)
        {
            timeToSpawn = Time.time + 1;
            Init();
        }

    }
    public GameObject SpawnEnemy()
    {
        float side = Random.Range(0, 4);
        float x1 = 0;
        float x2 = 0;
        float z1 = 0;
        float z2 = 0;

        switch (side)
        {
            case 0:
                x1 = -25;
                x2 = 25;
                z1 = 13;
                z2 = 20;
                break;
            case 1:
                x1 = 25;
                x2 = 28;
                z1 = -20;
                z2 = 20;
                break;
            case 2:
                x1 = -28;
                x2 = -25;
                z1 = -20;
                z2 = 20;
                break;
            case 3:
                x1 = -25;
                x2 = -25;
                z1 = -23;
                z2 = -17;
                break;
            default:
                Debug.Log("Error");
                break;
        }

                GameObject enemySpawned = Object.Instantiate(enemyData.enemyPrefab[Random.Range(0, enemyData.enemyPrefab.Length)],new Vector3(Random.Range(x1,x2), 1, Random.Range(z1, z2)) , Quaternion.identity);
        sceneData.totalEnemy++;

        return enemySpawned;
    }

}
