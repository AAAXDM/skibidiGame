using UnityEngine;

[RequireComponent (typeof(Obstacle))]
public class ObstacleParticles : MonoBehaviour
{
    [SerializeField] GameObject particlesObj;
    Particles particles;
    Obstacle obstacle;

    void Start()        
    {
        particles = particlesObj.GetComponent<Particles>();
        obstacle = GetComponent<Obstacle>();
        obstacle.CollisionAction += PlayParticle;
    }

    void OnDestroy() => obstacle.CollisionAction -= PlayParticle;

    void PlayParticle(Obstacle obstacle) => particles.PlayParticles(); 
}
