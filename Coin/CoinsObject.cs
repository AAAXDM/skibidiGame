using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent (typeof(CoinObstacle))]
public class CoinsObject : MonoBehaviour
{
    Coin[] coins;
    Vector3[] coinsPositions;
    CoinObstacle obstacle;
    List<float> coinsPosX;
    Dictionary <float, List<Coin>> positions;
    float coinMovedelay = 0.2f;

    public IReadOnlyCollection<Coin> Coins => coins;
    
    void Awake()
    { 
        coins = GetComponentsInChildren<Coin>(); 
        obstacle = GetComponent<CoinObstacle>();
        coinsPositions = new Vector3[coins.Length];

        for (int i = 0; i < coins.Length; i++)
        {
            coinsPositions[i] = coins[i].gameObject.transform.localPosition;
        }

        FillCoinsPosList();
        AddCoinStartDelay();
    }

    private void OnEnable()
    {
        obstacle.CollisionAction += ResetPositions;
        ChangeActiveAll(true);
        SubscribeToAll();
    }

    private void OnDisable()
    {
        obstacle.CollisionAction -= ResetPositions;
        UnsubScribeToAll();
        ChangeActiveAll(false);
    }

    void SubscribeToAll()
    {
        foreach (var coin in coins)
        {
            coin.Collision += Collision;
        }
    }

    void UnsubScribeToAll()
    {
        foreach (var coin in coins)
        {         
            coin.Collision -= Collision;
        }
    }

    void ResetPositions(Obstacle obstacle)
    {
        for (int i = 0; i < coins.Length; i++)
        {
           coins[i].gameObject.transform.localPosition = coinsPositions[i];
        }
    }

    void Collision(GameObject coin) => coin.SetActive(false);

    void FillCoinsPosList()
    {
        coinsPosX = new List<float>();
        positions = new Dictionary<float, List<Coin>>();

        for (int i = 0; i < coins.Length; i++)
        {
            int pos = coinsPosX.IndexOf(coins[i].transform.localPosition.x);
            if (pos == -1)
            {
                coinsPosX.Add(coins[i].transform.localPosition.x);
                List<Coin> coinsList = new() {coins[i]};
                positions.Add(coins[i].transform.localPosition.x, coinsList);
            }
            else
            {
                positions[coins[i].transform.localPosition.x].Add(coins[i]);
            }
        }

    }

    void AddCoinStartDelay()
    {
        float delay = 0;

        foreach (var coins in positions)
        { 
            foreach(var coin in coins.Value)
            {
                CoinMovement movement = coin.gameObject.GetComponent<CoinMovement>();
                movement.SetStartDelay(delay);
            }

            delay += coinMovedelay;
        }
    }

    public void ChangeActiveAll(bool active)
    {
        foreach (Coin coin in coins) 
        {
            coin.gameObject.SetActive(active);
        }
    }
}
