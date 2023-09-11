using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObstaclesSpawner : Spawner
{
    [SerializeField] protected float instantiateTime;
    [SerializeField] int obstaclesMaxCount;
    [SerializeField] protected ObstacleSO obstacleSO;
    [SerializeField] GameObject HiObstaclePos;

    protected Queue<Obstacle> obstacles;
    protected Queue<Obstacle> obstaclesInRun;
    protected int counter = 1;

    public float InstantiateTime => instantiateTime;
    public int Counter => counter;

    protected override void OnDisable()
    {
        base.OnDisable();
        Unsubscribe();
    }

    protected override void DoInStart()
    {
        StartSpawn();
        base.DoInStart();
    }

    protected override void DoInEnumerator()
    {
        base.DoInEnumerator();
        SetSpeed(obstaclesSpeed);
    }

    protected override void FillObstaclesList() => Fill();

    protected void Fill()
    {
        obstacles = new Queue<Obstacle>();
        obstaclesInRun = new Queue<Obstacle>();

        for (int i = 0; i < obstaclesMaxCount; i++)
        {
            GameObject obstacle = Instantiate(obstacleSO.GetRandomObstacle(), transform.position, transform.rotation);
            Obstacle obstacleComponent = GetObstacleComponent(obstacle);
            SetTime(obstacleComponent);
            obstacleComponent.SetPauseManager(uIController.PauseManager);
            obstacleComponent.CollisionAction += CollisionEvent;
            obstacles.Enqueue(obstacleComponent);
            obstacleComponent.gameObject.SetActive(false);
        }
    }

    protected virtual void SetTime(Obstacle obstacle) { }

    protected virtual Obstacle GetObstacleComponent(GameObject gameObject)
                                            => gameObject.GetComponent<Obstacle>();
    protected override void CollisionEvent(Obstacle obstacle)
    {
        if(obstaclesInRun.Count > 0) obstaclesInRun.Dequeue();
        obstacles.Enqueue(obstacle);
        DiactivateObtscle(obstacle);
    }

    protected virtual void SetRandom(Obstacle obstacle)
    {
        if (Random.value >= 0.5) obstacle.gameObject.transform.position = transform.position;
        else obstacle.gameObject.transform.position = HiObstaclePos.transform.position;
    }

    protected virtual void Unsubscribe(Obstacle obstacle) => obstacle.CollisionAction -= CollisionEvent;

    protected virtual void StartSpawn() => StartCoroutine(InstObstacle());

    protected virtual void Unsubscribe()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.CollisionAction -= CollisionEvent;
        }
        foreach (var obstacle in obstaclesInRun)
        {
            obstacle.CollisionAction -= CollisionEvent;
        }
    }

    protected virtual void DoInSpawn()
    {
        Obstacle obstacle = obstacles.Dequeue();
        obstaclesInRun.Enqueue(obstacle);
        ActivateObstacle(obstacle);
        obstacle.gameObject.SetActive(true);
        obstacle.transform.position = HiObstaclePos.transform.position;
        SetRandom(obstacle);
        obstacle.SetSpeedAndRun(obstaclesSpeed);
        counter++;
    }
    
    void SetSpeed(int speed)
    {
        foreach (var obstacle in obstaclesInRun)
        {
            obstacle.SetSpeedAndRun(speed);
        }
    }

    public override void PowerOff(int speed)
    {
        if (speed != 0)
        {
            base.PowerOff(speed);

            SetSpeed(obstaclesSpeed);
        }
    }

    public override void ChangeSpeedByPower(int speed)
    {
        if (speed != 0)
        {
            base.ChangeSpeedByPower(speed);

            SetSpeed(obstaclesSpeed);
        }
    }

    public void ResetCounter() => counter = 0;

    IEnumerator InstObstacle()
    {
        while (true)
        {
            yield return new Wait(instantiateTime,this);
            if (!isPaused)
            {
                DoInSpawn();
            }
        }
    }
}
