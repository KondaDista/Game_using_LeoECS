using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathSystem : IEcsRunSystem
{
    private EcsFilter<Enemy, DeathEvent> deadEnemies;
    private UI ui;
    private SceneData sceneData;

    public void Run()
    {
        foreach (var i in deadEnemies)
        {
            ref var enemy = ref deadEnemies.Get1(i);
            ref var entity = ref deadEnemies.GetEntity(i);
            ui.gameScreen.AddKills(1);
            sceneData.totalEnemy--;
            enemy.enemyGO.GetComponent<DestroyGO>().Destroy();
            entity.Destroy();
        }
    }
}