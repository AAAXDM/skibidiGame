using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerBlinker : MonoBehaviour,IPauseHandler
{
    [SerializeField]SpriteRenderer magnetSprite;
    SpriteRenderer sprite;
    PlayerStats playerStats;
    UIController uIController;
    Color startColor = Color.white;

    float invRepeatingTime = 0;
    float repeatRate = 0.5f;
    float invokeTime = 0.2f;
    float startBlinkValue = 0.5f;
    float endBlinkValue = 0.8f;
    float endBlicCoroutineValue = 1;
    float awaitTime = 0.1f;
    bool isPaused;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerStats = GetComponentInParent<PlayerStats>();
        magnetSprite.gameObject.SetActive(false);
    }

    void Start()
    {
        uIController = FindObjectOfType<UIController>();
        uIController.PauseManager.Register(this);
    }

    void OnEnable() => playerStats.LossLife += Blink;

    void OnDisable() => playerStats.LossLife -= Blink;

    void Blink()
    {
        InvokeRepeating("StartBlik", invRepeatingTime, repeatRate);
        Invoke("StopBlink", invokeTime);
    }

    void StartBlik()
    {
        Color clr = Color.white;
        float a = Random.Range(startBlinkValue, endBlinkValue);
        clr.a = a;
        sprite.color = clr;
    }

    void StopBlink()
    {
        CancelInvoke("StartBlik");
        sprite.color = startColor;
    }

    public void EndlessBlik(Color color) => StartCoroutine(Blik(color));

    public void StopEndlessBlink()
    {
        StopAllCoroutines();
        sprite.color = startColor;
    }

    public void ChangeMagnetSpriteState(bool state) => magnetSprite.gameObject.SetActive(state);

    IEnumerator Blik(Color color)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(awaitTime);

            if(!isPaused)
            {
                Color clr = color;
                float a = Random.Range(startBlinkValue, endBlicCoroutineValue);
                clr.a = a;
                sprite.color = clr;
            }
        }
    }

    public void SetPaused(bool isPaused) => this.isPaused = isPaused;

}
