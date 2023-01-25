using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class DeathScreenSystem : IEcsRunSystem
{
    private EcsFilter<PlayerDeathEvent> filter;
    private RuntimeData runtimeData;

    public void Run()
    {
        foreach (var i in filter)
        {
            if (runtimeData.gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
                continue;
            }

            filter.GetEntity(i).Del<PlayerDeathEvent>();
        }
    }
}