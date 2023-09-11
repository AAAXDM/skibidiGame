using UnityEngine;

public class CoinSpawner : ObstaclesSpawner
{
    protected override void SetRandom(Obstacle obstacle)
    {
        if (Random.value >= 0.7) return;
        else CollisionEvent(obstacle);
    }
}
