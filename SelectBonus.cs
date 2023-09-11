using UnityEngine;

public class SelectBonus : MonoBehaviour
{
    MoneyWheelPart[] parts;
    PlayerStats stats;
    int coins;

    void Awake()
    {
        parts = GetComponentsInChildren<MoneyWheelPart>();
        stats = FindObjectOfType<PlayerStats>();
    }

    void OnEnable()
    {
        foreach (var part in parts) 
        {
            part.Collision += GetCoins;
        }
    }

    void OnDisable()
    {
        foreach (var part in parts)
        {
            part.Collision -= GetCoins;
        }
    }

    void GetCoins(MoneyWheelPart wheelPart) => coins = wheelPart.Coins;

    public void GetBonus() => stats.AddCoins(coins);

}
