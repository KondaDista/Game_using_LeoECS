using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class BulletMoveSystem : IEcsRunSystem
{
    private EcsFilter<Bullet> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var bullet = ref filter.Get1(i);

            var position = bullet.bulletReady.transform.position;
            position += bullet.direction * bullet.speed * Time.deltaTime;
            bullet.bulletReady.transform.position = position;

            var displacementSinceLastFrame = position - bullet.previousPos;
            var hit = Physics.SphereCast(bullet.previousPos, bullet.radius, displacementSinceLastFrame.normalized, out var hitInfo, displacementSinceLastFrame.magnitude);

            if (hit)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var bulletHit = ref entity.Get<BulletHit>();
                bulletHit.raycastHit = hitInfo;
            }

            bullet.previousPos = bullet.bulletReady.transform.position;

        }
    }
}
