using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    PlayerStats stats;
    [SerializeField] TextMeshProUGUI coinsCount;
    [SerializeField] TextMeshProUGUI lifesCount;
    [SerializeField] TextMeshProUGUI score;

    void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
        lifesCount.text = stats.Lifes.ToString();
    }

    void OnEnable()
    {
        stats.AddScore += IncreaseScore;
        stats.LossLife += LossLife;
        stats.IncreaseCoin += AddCoin;
    }

    void OnDisable()
    {
        stats.AddScore -= IncreaseScore;
        stats.LossLife -= LossLife;
        stats.IncreaseCoin -= AddCoin;
    }

    void AddCoin()
    {
        coinsCount.text = stats.Coins.ToString();
    }

    void LossLife()
    {
        lifesCount.text = stats.Lifes.ToString();
    }

    void IncreaseScore()
    {
        score.text = stats.Score.ToString();
    }
}
