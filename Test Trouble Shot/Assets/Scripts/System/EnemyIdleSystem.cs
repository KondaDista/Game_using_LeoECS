using Leopotam.Ecs;
using UnityEngine;

public class EnemyIdleSystem : IEcsRunSystem
{
    private EcsFilter<Enemy, Idle> calmEnemies;
    private RuntimeData runtimeData;
    
    public void Run()
    {
        foreach (var i in calmEnemies)
        {
            ref var enemy = ref calmEnemies.Get1(i);
            ref var player = ref runtimeData.playerEntity.Get<Player>();

            ref var entity = ref calmEnemies.GetEntity(i);
            entity.Del<Idle>();
            ref var follow = ref entity.Get<Follow>();
            follow.target = runtimeData.playerEntity;

        }
    }
}