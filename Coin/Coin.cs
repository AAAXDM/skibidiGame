using System;
using UnityEngine;

[RequireComponent(typeof(CoinMovement))]
public class Coin : MonoBehaviour
{
    [SerializeField] GameObject particlesObject;
    [SerializeField] GameObject doubleParticleOpbject;
    Particles particles;
    Particles doubleParticles;
    CoinMovement coinMovement;
    CoinsObject coins;
    float moveDelta = 0.1f;

    public event Action<GameObject> Collision;

    private void Awake()
    {
        particles = particlesObject.GetComponent<Particles>();
        doubleParticles = doubleParticleOpbject.GetComponent<Particles>();
        coinMovement = GetComponent<CoinMovement>();
        coins = GetComponentInParent<CoinsObject>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        PowerObject magnet = collision.GetComponent<PowerObject>();
        if (magnet == null)
        {
            if (collision.gameObject.layer == 6) PlayParticle(doubleParticles);

            else PlayParticle(particles);
        }
    }

    void PlayParticle(Particles particles)
    {
        Collision.Invoke(this.gameObject);
        particles.PlayParticles();
    }

    public void StartGoToPlayer()
    {
        transform.parent = null;
        coinMovement.Stop();
    }

    public void GoToplayer(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        transform.Translate(direction * moveDelta);
    }

    public void GoBack() => transform.parent = coins.gameObject.transform;
}
