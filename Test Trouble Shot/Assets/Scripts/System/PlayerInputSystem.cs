using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsFilter<PlayerInputData, HasWeapon> filter;
    private EcsFilter<Player, DeathEvent> playerDead;
    private EcsWorld ecsWorld;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var input = ref filter.Get1(i);
            ref var hasWeapon = ref filter.Get2(i);

            input.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            if (Input.GetMouseButtonDown(0))
            {
                hasWeapon.weapon.Get<Shoot>();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ref var weapon = ref hasWeapon.weapon.Get<Weapon>();

                if (weapon.currentInMagazine < weapon.maxInMagazine)
                {
                    ref var reload = ref weapon.owner.Get<TryReload>();
                }
            }


        }

        foreach (var i in playerDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerDead.GetEntity(i).Destroy();
                ecsWorld.NewEntity().Get<PlayerDeathEvent>();
            }
        }
    }
}
