using Leopotam.Ecs;
using UnityEngine;

public class ReloadingSystem : IEcsRunSystem 
{
    private EcsFilter<Weapon, TryReload> reloadingFinishedFilter;
    private EcsFilter<Player, TryReload> reloadingPlayer;
    private UI ui;

    public void Run()
    {
        foreach (var i in reloadingPlayer)
        {
            ref var weapon = ref reloadingFinishedFilter.Get1(i);

            var needAmmo = weapon.maxInMagazine - weapon.currentInMagazine;
            weapon.currentInMagazine = (weapon.totalAmmo >= needAmmo)
                ? weapon.maxInMagazine
                : weapon.currentInMagazine + weapon.totalAmmo;
            weapon.totalAmmo -= needAmmo;
            weapon.totalAmmo = weapon.totalAmmo < 0
                ? 0
                : weapon.totalAmmo;
            ref var entity = ref reloadingPlayer.GetEntity(i);
            if (weapon.owner.Has<Player>())
            {
                ui.gameScreen.SetAmmo(weapon.currentInMagazine, weapon.totalAmmo);
            }

            entity.Del<TryReload>();
        }

        foreach (var i in reloadingFinishedFilter)
        {
            ref var weapon = ref reloadingFinishedFilter.Get1(i);

            var needAmmo = weapon.maxInMagazine - weapon.currentInMagazine;
            weapon.currentInMagazine = (weapon.totalAmmo >= needAmmo)
                ? weapon.maxInMagazine
                : weapon.currentInMagazine + weapon.totalAmmo;
            weapon.totalAmmo -= needAmmo;
            weapon.totalAmmo = weapon.totalAmmo < 0
                ? 0
                : weapon.totalAmmo;
            ref var entity = ref reloadingFinishedFilter.GetEntity(i);
            if (weapon.owner.Has<Player>())
            {
                ui.gameScreen.SetAmmo(weapon.currentInMagazine, weapon.totalAmmo);
            }

            entity.Del<TryReload>();
        }

    }
}