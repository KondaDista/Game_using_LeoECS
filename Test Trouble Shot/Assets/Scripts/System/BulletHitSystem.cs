using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class BulletHitSystem : IEcsRunSystem
{
    private EcsFilter<Bullet, BulletHit> filter;
    private EcsWorld ecsWorld;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var bullet = ref filter.Get1(i);
            ref var hit = ref filter.Get2(i);

            if (hit.raycastHit.collider.gameObject.TryGetComponent(out EnemyView enemyView))
            {
                if (enemyView.entity.IsAlive())
                {
                    ref var e = ref ecsWorld.NewEntity().Get<DamageEvent>();
                    e.target = enemyView.entity;
                    e.value = bullet.damage * enemyView.startArmor;
                }
            }

            bullet.bulletReady.GetComponent<DestroyGO>().Destroy();
            filter.GetEntity(i).Destroy();
        }
    }
}