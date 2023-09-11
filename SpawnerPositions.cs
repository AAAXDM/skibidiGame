using UnityEngine;

public class SpawnerPositions : MonoBehaviour
{
    [SerializeField] GameObject obstacleSpawner;
    [SerializeField] GameObject obstacleSpawnHiPoint;
    [SerializeField] GameObject coinSpawner;
    [SerializeField] float xObstcaleDelta;
    [SerializeField] float xCoinDelta;

    void Awake()
    {
        Vector3 cameraRange =  Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        float x = cameraRange.x;
        float y = obstacleSpawner.transform.position.y;
        float z = obstacleSpawner.transform.position.z;
        obstacleSpawner.transform.position = new Vector3(x + xObstcaleDelta, y, z);
        y = obstacleSpawnHiPoint.transform.position.y;
        obstacleSpawnHiPoint.transform.position = new Vector3(x + xObstcaleDelta, y, z);
        y = coinSpawner.transform.position.y;
        z = coinSpawner.transform.position.z;
        coinSpawner.transform.position = new Vector3(x + xCoinDelta, y, z);
    }


}
