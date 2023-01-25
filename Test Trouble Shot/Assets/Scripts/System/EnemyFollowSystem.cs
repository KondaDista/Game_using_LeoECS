using Leopotam.Ecs;
using UnityEngine;

public class EnemyFollowSystem : IEcsRunSystem
{
    private EcsFilter<Enemy, Follow> followingEnemies;
    
    private EcsWorld ecsWorld;
        
    public void Run()
    {
        foreach (var i in followingEnemies)
        {
            ref var enemy = ref followingEnemies.Get1(i);
            ref var follow = ref followingEnemies.Get2(i);

            if (!follow.target.IsAlive())
            {
                ref var entity = ref followingEnemies.GetEntity(i);
                entity.Del<Follow>();
                continue;
            }
            
            ref var transformRef = ref follow.target.Get<TransformRef>();
            var targetPos = transformRef.transform.position;
            enemy.navMeshAgent.SetDestination(targetPos);
            var direction = (targetPos - enemy.transform.position).normalized;
            direction.y = 0f;
            enemy.transform.forward = direction;

            if ((enemy.transform.position - transformRef.transform.position).sqrMagnitude <= enemy.meleeAttackDistance && Time.time >= follow.nextAttackTime)
            {
                follow.nextAttackTime = Time.time + enemy.meleeAttackInterval;
                enemy.navMeshAgent.isStopped = true;
                ref var e = ref ecsWorld.NewEntity().Get<DamageEvent>();
                e.target = follow.target;
                e.value = enemy.damage * follow.target.Get<Health>().armor;
            }
            else if ((enemy.transform.position - transformRef.transform.position).sqrMagnitude > enemy.meleeAttackDistance)
            {
                enemy.navMeshAgent.isStopped = false;
            }
        }
    }
}