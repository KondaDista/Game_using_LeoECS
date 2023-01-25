using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class ECS : MonoBehaviour
{
    public StaticData configuration;
    public EnemyData configurationEnemy;
    public SceneData sceneData;
    public UI ui;

    private EcsWorld ecsWorld;
    private EcsSystems updateSystems;
    private EcsSystems fixedUpdateSystems;

    private void Start()
    {
        ecsWorld = new EcsWorld();
        updateSystems = new EcsSystems(ecsWorld);
        fixedUpdateSystems = new EcsSystems(ecsWorld);
        RuntimeData runtimeData = new RuntimeData();

        updateSystems
            .Add(new PlayerInitSystem())
            .Add(new EnemyInitSystem())
            .OneFrame<TryReload>()
            .Add(new PlayerInputSystem())
            .Add(new DeathScreenSystem())
            .Add(new PlayerRotationSystem())
            .Add(new EnemyIdleSystem())
            .Add(new EnemyFollowSystem())
            .Add(new DamageSystem())
            .Add(new EnemyDeathSystem())
            .Add(new PlayerDeathSystem())
            .Add(new WeaponShootSystem())
            .Add(new SpawnBulletSystem())
            .Add(new BulletMoveSystem())
            .Add(new BulletHitSystem())
            .Add(new ReloadingSystem())
            .Inject(configuration)
            .Inject(configurationEnemy)
            .Inject(sceneData)
            .Inject(ui)
            .Inject(runtimeData);

        fixedUpdateSystems
            .Add(new PlayerMoveSystem())
            .Inject(configuration)
            .Inject(configurationEnemy)
            .Inject(sceneData)
            .Inject(runtimeData);

        updateSystems.Init();
        fixedUpdateSystems.Init();
    }

    private void Update()
    {
        updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        fixedUpdateSystems?.Run(); 
    }

    private void OnDestroy()
    {
        ecsWorld.Destroy();
        updateSystems.Destroy();
    }
}
