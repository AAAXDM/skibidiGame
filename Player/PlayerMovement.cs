using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour,IPauseHandler
{
    [SerializeField] GameObject toilet;
    [SerializeField] float jumpSpeed;
    [SerializeField] float sppedDelta;
    [SerializeField] float fall;
    [SerializeField] float lowJump;
    [SerializeField] float holdTime;
    [SerializeField] GameObject windParticles;
    ParticleSystem windParticlesystem;
    ParticleSystem particleSystem;
    PlayerStats playerStats;
    LevelSpawner levelSpawner;
    UIController uIController;
    HeadMovement headMovement;
    PlayerAction action;
    Rigidbody2D rb;
    Vector3 savedVelocity;
    float hold;
    int floorLayer = 14;
    bool canJump;
    bool isPaused;
    bool isOnJump;

    CollisionDelegate CollisionWithObstacle;
    TriggerDelegate TriggerPower;

    public Action<SpecialPower> GetPower;

    void Awake()
    {
        action = new PlayerAction();
        rb = GetComponent<Rigidbody2D>();
        headMovement = GetComponentInChildren<HeadMovement>();
        playerStats = GetComponent<PlayerStats>();
        levelSpawner = FindObjectOfType<LevelSpawner>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        windParticlesystem = windParticles.GetComponent<ParticleSystem>();
        windParticles.SetActive(false);
        SetDelegateBack();
        SetSkin();
        hold = holdTime;
        canJump = true;
    }

    void Start()
    {
        uIController = FindObjectOfType<UIController>();
        uIController.PauseManager.Register(this);
        uIController.PauseManager.Register(headMovement);
        uIController.PauseManager.Register(playerStats);
    }

    void OnEnable()
    {
        action.Player.Jump.performed += Jump;
        action.Player.Jump.Enable();
        action.Player.GetDown.performed += GetDown;
        action.Player.GetDown.Enable();
        levelSpawner.ChangeSpeed += ChangeSpeed;
        headMovement.PlayParticles += PlayParticles;
    }

    void OnDisable()
    {
        action.Player.Jump.performed -= Jump;
        action.Player.Jump.Disable();
        action.Player.GetDown.performed -= GetDown;
        action.Player.GetDown.Disable();
        levelSpawner.ChangeSpeed -= ChangeSpeed;
        headMovement.PlayParticles += PlayParticles;
        UnregeasterPauseHandlers();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obstacle = collision.collider.GetComponent<Obstacle>();

        if(obstacle != null) CollisionWithObstacle(obstacle);

        if(collision.gameObject.layer == floorLayer) canJump = true;
    }

    public void OnTriggerEnter2D(Collider2D collision) => TriggerPower();

    void Jump(InputAction.CallbackContext context)
    {
        if (canJump && !isPaused)
        {
            StartCoroutine(JumpCoroutine(context));
            StartCoroutine(JumpProcessCoroutine());
            canJump = false;
        }
    }

    void GetDown(InputAction.CallbackContext context)
    {
        if (!isPaused) headMovement.HeadDown(context);
    }

    void UnregeasterPauseHandlers()
    {
        uIController.PauseManager.UnRegister(this);
        uIController.PauseManager.UnRegister(headMovement);
        uIController.PauseManager.UnRegister(playerStats);
    }

    void ChangeSpeed()
    {
        jumpSpeed += sppedDelta;
        headMovement.ChangeSpeed();
    }

    void Collision(Obstacle obstacle)
    {

        if (obstacle.Type == ObstacleType.Box)
        {
            playerStats.DecreaseLife();
            return;
        }

        if (obstacle.Type == ObstacleType.Power)
        {
            SpecialPower ipower = obstacle.GetComponent<SpecialPower>();
            if (ipower.PowerType == PowerType.speed) windParticles.SetActive(true);
            CollisionWithObstacle = ipower.CollisionDelegate;
            TriggerPower = ipower.TriggerDelegate;
            GetPower(ipower);
        }
    }

    void SetSkin()
    {
        if (PlayerProgress.Instance.SummaryStats.Toilet != null)
        {
           SpriteRenderer headsprite =  headMovement.gameObject.GetComponent<SpriteRenderer>();
           headsprite.sprite = PlayerProgress.Instance.SummaryStats.Head;
           SpriteRenderer toiletSprite = toilet.gameObject.GetComponent<SpriteRenderer>();
           toiletSprite.sprite = PlayerProgress.Instance.SummaryStats.Toilet;
        }
    }

    void Trigger() => playerStats.AddCoin();
    void PlayParticles() => particleSystem.Play();

    public void SetDelegateBack()
    {
        CollisionWithObstacle = Collision;
        TriggerPower = Trigger;
        windParticles.SetActive(false);
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;

        if (isPaused)
        {
            StopAllCoroutines();
            savedVelocity = rb.velocity;
            rb.isKinematic = isPaused;
            rb.velocity = Vector2.zero;
            if (windParticles.activeInHierarchy) windParticlesystem.Pause();
        }

        else
        {
            rb.isKinematic = isPaused;
            rb.AddForce(savedVelocity,ForceMode2D.Impulse);
            if (windParticles.activeInHierarchy) windParticlesystem.Play();
            if (isOnJump) StartCoroutine(JumpProcessCoroutine());
        }
    }

    IEnumerator JumpCoroutine(InputAction.CallbackContext context)
    {
        float value;
        bool isInJump = true;

        while (isInJump && hold > 0)
        {
            hold -= Time.deltaTime;
            value = context.ReadValue<float>();
            if (value < 1) isInJump = false;
            yield return new WaitForFixedUpdate();
            rb.velocity = Vector2.up * jumpSpeed;
        }

        hold = holdTime;
    }

    IEnumerator JumpProcessCoroutine()
    {
        isOnJump = true;
        yield return new WaitForFixedUpdate();

        while (rb.velocity.y != 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
            }
            if (rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJump - 1) * Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        isOnJump = false;
    }
}
