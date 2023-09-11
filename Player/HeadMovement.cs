using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class HeadMovement : MonoBehaviour, IPauseHandler
{
    [SerializeField] float moveTime;
    [SerializeField] float moveTimeDelta;
    CallbackContext context;

    Vector3 delta = new(0, 0.01f, 0f);
    float holdTime = 0.5f;
    float maxdown = -0.6f;
    float maxup = 0.2f;
    float hold;
    bool canDive = true;

    public Action PlayParticles;

    void Awake() => hold = holdTime;

    public void HeadDown(InputAction.CallbackContext context)
    {
        if (canDive)
        {
            StartCoroutine(GoDown(context));
        }
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused) StopAllCoroutines();

        else if (!canDive) StartCoroutine(GoDown(context));
    }

    public void ChangeSpeed() => moveTime -= moveTimeDelta;

    IEnumerator GoDown(InputAction.CallbackContext context)
    {
        Vector3 localpos = transform.localPosition;
        float value;
        this.context = context;
        canDive = false;
        bool canSit = true;

        while (localpos.y > maxdown)
        {
            yield return new WaitForSecondsRealtime(moveTime * Time.deltaTime);
            transform.localPosition -= delta;
            localpos = transform.localPosition;
        }

        PlayParticles();

        while (canSit && hold > 0)
        {
            hold -= Time.deltaTime;
            value = context.ReadValue<float>();
            if (value < 1) canSit = false;
            yield return new WaitForFixedUpdate();
        }

        while (localpos.y < maxup)
        {
            yield return new WaitForSecondsRealtime(moveTime * Time.deltaTime);
            transform.localPosition += delta;
            localpos = transform.localPosition;
        }

        canDive = true;
        hold = holdTime;
    }
}
