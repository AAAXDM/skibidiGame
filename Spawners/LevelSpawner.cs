using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSpawner : Spawner
{
    [SerializeField] List<GameObject> objects;

    List<Obstacle> obstacles;
    Obstacle posReference;
    float delta = 0.07f;
    float width;
    int counter;

    public Action ChangeSpeed;

    protected override void OnDisable()
    {
        base.OnDisable();
        foreach(var obstacle in obstacles)
        {
            obstacle.CollisionAction -= CollisionEvent;
        }
    }

    protected override void DoInStart()
    {
        base.DoInStart();
        counter = 0;
    }

    protected override void DoInEnumerator()
    {
        base.DoInEnumerator();
        ChangeObstacleSpeed();
        ChangeSpeed();
    }

    protected override void FillObstaclesList()
    {
        obstacles = new List<Obstacle>();

        foreach (var obj in objects)
        {
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            if (obstacle == null) throw new Exception("No obstacle component in GameObject");
            obstacle.CollisionAction += CollisionEvent;
            obstacles.Add(obstacle);
        }

        for (int i = 0; i < obstacles.Count; i++)
        {
            Obstacle obstacle = obstacles[i];
            if (i == 0) CalculateObjectWidth(obstacle.gameObject);
            MapPart obstacleComponent = obstacle.gameObject.GetComponent<MapPart>();
            obstacleComponent.SetPauseManager(uIController.PauseManager);
            obstacle.SetSpeed(obstaclesSpeed);
            posReference = obstacle;
        }
    }

    void InstObstacle()
    {
        Obstacle obstacle = obstacles[counter];
        SetCounter();
        SetPosition(obstacle.gameObject);
        ActivateObstacle(obstacle);
    }

    protected override void CollisionEvent(Obstacle obstacle)
    {
        DiactivateObtscle(obstacle);
        InstObstacle();
        obstacle.gameObject.SetActive(true);
        obstacle.SetSpeed(obstaclesSpeed);
        posReference = obstacle;
    }

    void CalculateObjectWidth(GameObject gameObject)
    {
        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
        if (box == null) throw new Exception("No box collider2D");
        width = box.size.x;
    }

    void SetPosition(GameObject gameObject)
    {
        Vector3 pos = posReference.gameObject.transform.position + new Vector3(width + delta, 0, 0);      
        gameObject.transform.position = pos;
    }

    void SetCounter()
    {
        if (counter < obstacles.Count - 1) counter++;
        else counter = 0;
    }

    void ChangeObstacleSpeed()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetSpeedAndRun(obstaclesSpeed);
        }
    }

    public override void ChangeSpeedByPower(int speed)
    {
        if (speed != 0)
        {
            base.ChangeSpeedByPower(speed);
            ChangeObstacleSpeed();
        }
    }

    public override void PowerOff(int speed)
    {
        if (speed != 0)
        {
            base.PowerOff(speed);
            ChangeObstacleSpeed();
        }
    }
}
