using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class PlayerRotationSystem : IEcsRunSystem
{
    private EcsFilter<Player, PlayerInputData> filter;
    private SceneData sceneData;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var player = ref filter.Get1(i);

            Plane playerPlane = new Plane(Vector3.up, player.playerTransform.position);
            Ray ray = sceneData.mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!playerPlane.Raycast(ray, out var hitDistance)) continue;

            player.playerTransform.forward = ray.GetPoint(hitDistance) - player.playerTransform.position;
        }
    }
}
