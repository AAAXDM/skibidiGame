using System.Collections;
using UnityEngine;

public class PowerObject : MonoBehaviour
{
    [SerializeField] GameObject power;
    PowersCounter powerCounter;
    PlayerMovement player;
    PlayerBlinker blinker;
    SpecialPower ipower;
    BoxCollider2D collider;
    float coroutineTime = 0.01f;
    bool canMoveCoin;
    bool isOnPower;

    void Awake()
    {
        ipower = power.GetComponent<SpecialPower>();
        player = FindObjectOfType<PlayerMovement>();
        powerCounter = FindObjectOfType<PowersCounter>();
        blinker = player.GetComponentInChildren<PlayerBlinker>();
        collider = GetComponent<BoxCollider2D>();
    }

    void OnEnable() => ipower.SetPower += ActivatePower;

    void OnDisable() => ipower.SetPower -= ActivatePower;

    void FixedUpdate()
    {
        if (isOnPower) transform.position = player.transform.position;
        else transform.position = power.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Coin coin = collision.GetComponent<Coin>();

        if (coin != null)
        {
            canMoveCoin = true;
            coin.Collision += CanMoveCoin;
            coin.StartGoToPlayer();
            IEnumerator coroutine = GoToPlayer(coin);
            StartCoroutine(coroutine);
        }
    }

    void CanMoveCoin(GameObject coinObj)
    {
        canMoveCoin = false;
        Coin coin = coinObj.GetComponent<Coin>();
        coin.Collision -= CanMoveCoin;
    }

    void ActivatePower(SpecialPower ipower)
    {
        isOnPower = true;
        collider.enabled = true;
        powerCounter.EndPower += DiactivatePower;
        blinker.ChangeMagnetSpriteState(true);
    }


    void DiactivatePower()
    {
        isOnPower = false;
        collider.enabled = false;
        powerCounter.EndPower -= DiactivatePower;
        blinker.ChangeMagnetSpriteState(false);
    }

    IEnumerator GoToPlayer(Coin coin)
    {
        while (canMoveCoin)
        {
            coin.GoToplayer(player.transform.position);
            yield return new WaitForSecondsRealtime(coroutineTime);
        }

        coin.GoBack();
    }

}
