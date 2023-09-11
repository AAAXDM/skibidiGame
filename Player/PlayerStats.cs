using UnityEngine;
using System.Collections;
using System;

public class PlayerStats : MonoBehaviour,IPauseHandler
{
    [SerializeField] int lifes;
    [SerializeField] float increaseScoreTime;
    [SerializeField] int score;
    [SerializeField] int coins;

    public int Score => score;
    public int Lifes => lifes;
    public int Coins => coins;

    public Action LossLife;
    public Action AddScore;
    public Action IncreaseCoin;

    void Start() => StartCoroutine(IncreaseScore());

    public void DecreaseLife()
    {
        if(lifes > 0) lifes--;
        else EndGame();
        LossLife();
    }

    void EndGame()
    {

    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused) StopAllCoroutines();
        else StartCoroutine(IncreaseScore());
    }

    public void AddCoin()
    {
        coins++;
        IncreaseCoin();
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
        IncreaseCoin();
    }

    IEnumerator IncreaseScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseScoreTime);
            score++;
            AddScore();
        }
    }
}
