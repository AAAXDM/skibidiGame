using System;
using UnityEngine;

public class SpecialPower : Obstacle
{
    [SerializeField] Color color;
    [SerializeField] float actionTime;
    [SerializeField] int speedDelta;
    [SerializeField] PowerType powerType;
    protected PlayerStats stats;
    SpriteRenderer sprite;
    PowersCounter counter;

    public Color Color => color;
    public PowerType PowerType => powerType;
    public float ActionTime => actionTime;
    public int SpeedDelta => speedDelta;

    public event Action<SpecialPower> SetPower;
    public event Action<SpecialPower> RemovePower;
    public CollisionDelegate CollisionDelegate => Collision;
    public TriggerDelegate TriggerDelegate => Trigger;

    protected override void Awake()
    {
        base.Awake();
        stats = FindObjectOfType<PlayerStats>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        counter = FindObjectOfType<PowersCounter>();
    }

    void Start()
    {
        if (GetComponentInParent<PowerObject>() != null)
        {
            transform.SetParent(null);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == stats.gameObject)
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (!counter.IsPowerOn)
            {
                Stop();
                sprite.gameObject.SetActive(false);
                SetPower(this);
                counter.EndPower += DeletePower;
            }
        }

        base.OnCollisionEnter2D(collision);
    }

    void DeletePower()
    {
        RemovePower(this);
        counter.EndPower -= DeletePower;
    }

    protected virtual void Collision(Obstacle obstacle)
    {
        if (obstacle.Type == ObstacleType.Box)
        {
            stats.DecreaseLife();
            return;
        }
    }

    protected virtual void Trigger() => stats.AddCoin();

    public void InctreaseActionTime(float time)
    {
        if(time > 0) actionTime += time;
    }

    public void SetActionTime(float time) => actionTime = time;

}
