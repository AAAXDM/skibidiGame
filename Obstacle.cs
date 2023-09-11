using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour, IPauseHandler
{
    [SerializeField] ObstacleType type;
    protected PauseManager manager;
    protected Rigidbody2D rb;
    [SerializeField] protected int speed;

    public ObstacleType Type => type;

    public Action<Obstacle> CollisionAction;

    protected virtual void Awake() => rb = GetComponent<Rigidbody2D>();

    void OnDisable() => Stop();

    void OnDestroy() => manager.UnRegister(this);

    protected virtual void OnCollisionEnter2D(Collision2D collision) => CollisionAction.Invoke(this);

    protected void AddForce()
    {
        if (rb.totalForce == Vector2.zero) rb.AddRelativeForce(Vector2.left * speed);
    }

    public virtual void SetSpeed(int speed) => this.speed = speed;

    public void SetSpeedAndRun(int speed)
    {
        Stop();
        this.speed = speed;
        AddForce();
    }

    public void Stop() => rb.velocity = Vector2.zero;

    public virtual void SetPauseManager(PauseManager pauseManager)
    {
        manager = pauseManager;
        manager.Register(this);
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused) Stop();
        else AddForce();
    }
}
