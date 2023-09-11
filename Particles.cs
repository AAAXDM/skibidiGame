using System.Collections;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField]Transform parentTranform;
    ParticleSystem particles;

    float invokeTime = 2;

    void Start() => particles = GetComponentInChildren<ParticleSystem>();

    public void PlayParticles()
    {
        transform.parent = null;
        particles.Play();
        StartCoroutine(GetBack());
    }

    IEnumerator GetBack()
    {
        yield return new WaitForSecondsRealtime(invokeTime);
        transform.position = parentTranform.position;
        transform.parent = parentTranform;
    }
}
