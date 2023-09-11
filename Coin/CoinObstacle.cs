using UnityEngine;

[RequireComponent (typeof(CoinsObject))]
public class CoinObstacle : Obstacle
{
    GameObject endPoint;
    CoinsObject coinsObject;

    protected override void Awake()
    {
        base.Awake();
        endPoint = FindObjectOfType<EndPoint>().gameObject;
        coinsObject = GetComponent<CoinsObject>();
    }

    private void Update()
    {
        if (transform.position.x < endPoint.transform.position.x)
        {
            CollisionAction.Invoke(this);
        }
    }

    public override void SetPauseManager(PauseManager pauseManager)
    {
        base.SetPauseManager(pauseManager);

        foreach(var coin in coinsObject.Coins)
        {
            CoinMovement coinMovement = coin.gameObject.GetComponent<CoinMovement>();
            coinMovement.SetPauseManager(pauseManager);
        }
    }
}
