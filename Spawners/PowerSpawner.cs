using UnityEngine;
using Shop;

public class PowerSpawner : ObstaclesSpawner
{
    [SerializeField] ObstaclesSpawner spawner;
    Spawner[] spawners;

    void Awake() => spawners = FindObjectsOfType<Spawner>();

    protected override Obstacle GetObstacleComponent(GameObject gameObject)
    {
        Obstacle obstacle =  gameObject.GetComponentInChildren<Obstacle>();
        SpecialPower ipower = obstacle as SpecialPower;
        if (ipower != null)
        {
            ipower.SetPower += SetPower;
            ipower.RemovePower += RemovePower;
        }
        return obstacle;
    }

    protected override void DoInStart()
    {
        int i = 0;
        foreach (var pair in PlayerProgress.Instance.SummaryStats.ByuingPowers)
        {
            Item obstacle = pair.Key;
            PowerTime time = pair.Value;


            if (obstacle.IsByuing)
            {
                obstacleSO.AddToObstaclesList(GameManager.Instance.AllPowers.GetObstacle(i));
            }
            i++;
        }

        base.DoInStart();
    }

    protected override void StartSpawn()
    {
        if (obstacleSO.Obstacles.Count != 0) base.StartSpawn();
    }

    protected override void FillObstaclesList()
    {
        if (obstacleSO.Obstacles.Count != 0) Fill();
    }

    protected override void DoInSpawn()
    {
        if (counter * instantiateTime == spawner.Counter * spawner.InstantiateTime)
        {
            counter = 1;
            spawner.ResetCounter();
        }

        else
        {
            base.DoInSpawn();
        }
    }

    void SetPower(SpecialPower ipower)
    {
        foreach(var spawner in spawners)
        {
            spawner.ChangeSpeedByPower(ipower.SpeedDelta);
        }
    }

    void RemovePower(SpecialPower ipower)
    {
        foreach (var spawner in spawners)
        {
            spawner.PowerOff(ipower.SpeedDelta);
        }
    }

    protected override void Unsubscribe(Obstacle obstacle)
    {        
        base.Unsubscribe(obstacle);
        SpecialPower ipower = obstacle as SpecialPower;
        if (ipower != null)
        {
            ipower.SetPower -= SetPower;
            ipower.RemovePower -= RemovePower;
        }
    }

    protected override void DoInEnumerator()
    {
        if (obstacleSO.Obstacles.Count != 0) base.DoInEnumerator();
    }

    protected override void Unsubscribe()
    {
        if (obstacleSO.Obstacles.Count != 0) base.Unsubscribe();
    }

    protected override void SetTime(Obstacle obstacle) 
    {
        SpecialPower ipower = obstacle as SpecialPower;
        if (ipower != null)
        {
            int id = (int)ipower.PowerType;
            ipower.SetActionTime
                (PlayerProgress.Instance.SummaryStats.GetPowerTime(id).Time);
        }
    }
}
