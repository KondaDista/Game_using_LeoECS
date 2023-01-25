using Leopotam.Ecs;

public class DamageSystem : IEcsRunSystem
{
    private EcsFilter<DamageEvent> damageEvents;
    private UI ui;

    public void Run()
    {
        foreach (var i in damageEvents)
        {
            ref var e = ref damageEvents.Get1(i);
            ref var health = ref e.target.Get<Health>();

            health.health -= e.value;

            if (e.target.Has<Player>())
            {
                ui.gameScreen.SetHealth(health.health);
            }

            if (health.health <= 0)
            {
                e.target.Get<DeathEvent>();
            }

            damageEvents.GetEntity(i).Destroy();
        }
    }
}