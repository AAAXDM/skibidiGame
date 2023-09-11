using System;
using UnityEngine;

public class PowersCounter : MonoBehaviour, IPauseHandler
{
    PlayerMovement player;
    PlayerBlinker blinker;
    UIController uIController;
    Color color;
    float time;
    bool isPowerOn;
    bool isPaused;

    public bool IsPowerOn => isPowerOn;
    public Action EndPower;

    void Awake() => player = FindObjectOfType<PlayerMovement>();

    void Start()
    {
        blinker = player.GetComponentInChildren<PlayerBlinker>();
        uIController = FindObjectOfType<UIController>();
        uIController.PauseManager.Register(this);
    }

    void OnEnable() => player.GetPower += TurnOnPower;

    void OnDisable()
    {
        player.GetPower -= TurnOnPower;
        uIController.PauseManager.UnRegister(this);
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            if (isPowerOn && time > 0)
            {
                time -= Time.deltaTime;
            }
            if (isPowerOn && time <= 0)
            {
                TurnOffPower();
                EndPower();
                player.SetDelegateBack();
            }
        }
    }

    void TurnOnPower(SpecialPower ipower)
    {
        isPowerOn = true;
        time = ipower.ActionTime;
        color = ipower.Color;
        Blink();
    }

    void TurnOffPower()
    {
        StopBlik();
        isPowerOn = false;
        time = 0;
    }

    void Blink()
    {
        if (blinker != null) blinker.EndlessBlik(color);
    }

    void StopBlik()
    {
        if (blinker != null) blinker.StopEndlessBlink();
    }

    public void SetPaused(bool isPaused) => this.isPaused = isPaused;
}
