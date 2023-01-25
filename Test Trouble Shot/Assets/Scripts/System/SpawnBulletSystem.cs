using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class SpawnBulletSystem : IEcsRunSystem
{
    private EcsFilter<Weapon, SpawnBullet> filter;
    private EcsWorld ecsWorld;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var weapon = ref filter.Get1(i);

            var bulletReady = Object.Instantiate(weapon.bulletPrefab, weapon.bulletSpawnTransform.position, weapon.bulletSpawnTransform.rotation);
            var bulletEntity = ecsWorld.NewEntity();

            ref var bullet = ref bulletEntity.Get<Bullet>();

            bullet.damage = weapon.weaponDamage;
            bullet.direction = weapon.bulletSpawnTransform.forward;
            bullet.radius = weapon.bulletRadius;
            bullet.speed = weapon.bulletSpeed;
            bullet.previousPos = bulletReady.transform.position;
            bullet.bulletReady = bulletReady;

            ref var entity = ref filter.GetEntity(i);
            entity.Del<SpawnBullet>();
        }
    }
}
