using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObstacleSO", menuName = "Obstacles", order = 51)]
public class ObstacleSO : ScriptableObject
{
    [SerializeField] List<GameObject> obstacles;

    public IReadOnlyCollection<GameObject> Obstacles => obstacles;

    public GameObject GetRandomObstacle() => obstacles[Random.Range(0, obstacles.Count)];

    public GameObject GetObstacle(int i) => obstacles[i];

    public void AddToObstaclesList(GameObject obstacle) => obstacles.Add(obstacle);
}
