using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private StaticData staticData;
    private SceneData sceneData;
    private UI ui;
    private RuntimeData runtimeData;

    public void Init()
    {
        EcsEntity playerEntity = ecsWorld.NewEntity();
        ref var player = ref playerEntity.Get<Player>();
        ref var health = ref playerEntity.Get<Health>();
        ref var inputData = ref playerEntity.Get<PlayerInputData>();
        ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
        ref var transformRef = ref playerEntity.Get<TransformRef>();

        health.health = staticData.playerHealth;
        health.armor = staticData.playerArmor;
        player.playerSpeed = staticData.playerSpeed;

        GameObject playerReady = Object.Instantiate(staticData.playerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
        player.playerRigidbody = playerReady.GetComponent<Rigidbody>();
        player.playerTransform = playerReady.transform;
        player.playerSpeed = staticData.playerSpeed;

        var weaponEntity = ecsWorld.NewEntity();
        var weaponView = playerReady.GetComponent<WeaponSettings>();
        ref var weapon = ref weaponEntity.Get<Weapon>();
        weapon.owner = playerEntity;
        weapon.bulletPrefab = weaponView.bulletPrefab;
        weapon.bulletRadius = weaponView.bulletRadius;
        weapon.bulletSpawnTransform = weaponView.bulletSpawnTransform;
        weapon.bulletSpeed = weaponView.bulletSpeed;
        weapon.totalAmmo = weaponView.totalAmmo;
        weapon.weaponDamage = weaponView.weaponDamage;
        weapon.currentInMagazine = weaponView.currentInMagazine;
        weapon.maxInMagazine = weaponView.maxInMagazine;

        transformRef.transform = playerReady.transform;
        runtimeData.playerEntity = playerEntity;

        ui.gameScreen.SetAmmo(weapon.currentInMagazine, weapon.totalAmmo);
        ui.gameScreen.SetHealth(health.health);

        hasWeapon.weapon = weaponEntity;

    }
}
