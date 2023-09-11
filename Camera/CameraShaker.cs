using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] float shakeAmount;
    Camera camera;
    PlayerStats playerStats;
    Vector3 position;

    float invRepeatingTime = 0;
    float repeatRate = 0.01f;
    float invokeTime = 0.2f;

    void Awake()
    {
        camera = Camera.main;
        playerStats = FindObjectOfType<PlayerStats>();
        position = transform.position;
    }

    void OnEnable() => playerStats.LossLife += Shake;

    void OnDisable() => playerStats.LossLife -= Shake;

    void Shake()
    {
        InvokeRepeating("BeginShake", invRepeatingTime, repeatRate);
        Invoke("StopShake",invokeTime);
    }

    void BeginShake()
    {
        Vector3 cameraPos = camera.transform.position;

        float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
        float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
        cameraPos.x = offsetX;
        cameraPos.y = offsetY;

        camera.transform.position = cameraPos;
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        camera.transform.position = position;
    }
}
