using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class WeaponShootSystem : IEcsRunSystem
{
    private EcsFilter<Weapon, Shoot> filter;
    private UI ui;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var weapon = ref filter.Get1(i);

            ref var entity = ref filter.GetEntity(i);
            entity.Del<Shoot>();

            if (weapon.currentInMagazine > 0)
            {
                weapon.currentInMagazine--;

                if (weapon.owner.Has<Player>())
                {                    
                    ui.gameScreen.SetAmmo(weapon.currentInMagazine, weapon.totalAmmo);
                }
                ref var spawnBullet = ref entity.Get<SpawnBullet>();
            }
            else 
            {
                ref var reload = ref entity.Get<TryReload>();
            }
        }
    }
}
