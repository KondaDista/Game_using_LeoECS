using Leopotam.Ecs;
using UnityEngine;

public class PlayerDeathSystem : IEcsRunSystem
{
    private EcsFilter<Player, DeathEvent> deadPlayers;
    private RuntimeData runtimeData;
    private UI ui;

    public void Run()
    {
        foreach (var i in deadPlayers)
        {
            runtimeData.gameOver = true;
            ui.deathScreen.Show();
            deadPlayers.GetEntity(i).Del<PlayerInputData>();

            Time.timeScale = 0;
        }
    }
}