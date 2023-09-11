using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour, IPauseHandler
{
    [SerializeField] protected float changeSpeedTime;
    [SerializeField] protected int speedDelta;
    [SerializeField] protected int obstaclesSpeed;
    protected int saveObstacleSpeed;
    protected bool isPaused;
    bool canChangeSpeed;

    protected UIController uIController;

    void Start() => DoInStart();

    protected virtual void OnDisable() => uIController.PauseManager.UnRegister(this);

    protected virtual void ActivateObstacle(Obstacle obstacle) => obstacle.gameObject.transform.rotation = transform.transform.rotation;

    protected virtual void DiactivateObtscle(Obstacle obstacle) => obstacle.gameObject.SetActive(false);

    protected virtual void DoInStart()
    {
        uIController = FindObjectOfType<UIController>();
        FillObstaclesList();
        uIController.PauseManager.Register(this);
        canChangeSpeed = true;
        StartCoroutine(ChangeSpeed());
    }

    protected virtual void DoInEnumerator() => obstaclesSpeed += speedDelta;

    protected virtual void FillObstaclesList() { }

    protected virtual void CollisionEvent(Obstacle obstacle) { }

    public virtual void SetPaused(bool isPaused) => this.isPaused = isPaused;

    public virtual void ChangeSpeedByPower(int speed)
    {
        if (speed != 0)
        {
            saveObstacleSpeed = obstaclesSpeed;
            obstaclesSpeed += speed;
        }
    }


    public virtual void PowerOff(int speed)
    {
        if (speed != 0) obstaclesSpeed = saveObstacleSpeed;
    }

    IEnumerator ChangeSpeed()
    {
        while (canChangeSpeed)
        {
            yield return new Wait(changeSpeedTime, this);

            if(!isPaused) DoInEnumerator();
        }
    }
}
