using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class Chaser : MonoBehaviour, IPauseHandler
{
    [SerializeField] List<GameObject> positions;
    UIController uIController;
    Animator animator;
    PlayerStats playerStats;
    int counter;
    
    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        animator = GetComponent<Animator>();
        counter = 0;
    }

    private void Start()
    {
        uIController = FindObjectOfType<UIController>();
        uIController.PauseManager.Register(this);
    }

    void OnEnable() => playerStats.LossLife += SetPosition;

    void OnDisable()
    {
        playerStats.LossLife -= SetPosition;
        uIController.PauseManager.UnRegister(this);
    }

    void SetPosition()
    {
        if(counter < positions.Count) transform.position = positions[counter].transform.position;
        counter++;
    }

    public void SetPaused(bool isPaused) => animator.SetBool("Stop", isPaused);
}
