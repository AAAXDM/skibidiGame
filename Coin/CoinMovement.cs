using UnityEngine;
using System.Collections;

public class CoinMovement : MonoBehaviour, IPauseHandler
{
    [SerializeField] float moveTime;
    [SerializeField] float moveDelta;

    PauseManager manager;
    Vector3 delta = new(0, 0.01f, 0f);
    float startDelay;
    bool isGoDown;
    bool isActive;
    bool isAfterPause;

    void OnEnable() => StartMove();

    void OnDisable()
    {
        Stop();
    }

    void OnDestroy()  => manager.UnRegister(this);

    bool WhileChecker(float y, float pos)
    {
        if(isGoDown)
        {
            if (y > pos) return true;
            else return false;
        }
        else
        {
            if (y < pos) return true;
            else return false;
        }
    }

    public void StartMove()
    {
        isActive = true;
        isGoDown = true;
        StartCoroutine(Move());
    }
    
     
    public void SetStartDelay(float delay) => startDelay = delay;

    public void SetPauseManager(PauseManager pauseManager)
    {
        manager = pauseManager;
        manager.Register(this);
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            StopAllCoroutines();
            isAfterPause = true;
        }

        if(!isPaused && isActive) StartCoroutine(Move());
        if(!isPaused && !isActive)
        {
            isAfterPause = false;
        }
    }

    public void Stop()
    {
        isActive = false;
        StopAllCoroutines();
    }

    IEnumerator Move()
    {
        if (!isAfterPause)
        {
            yield return new WaitForSecondsRealtime(startDelay);
        }

        StartCoroutine(ChangePosition());
        isAfterPause = false;
    }

    IEnumerator ChangePosition()
    {
        float deltaY;
        Vector3 deltaFramePosition;

        while (true)
        {
            if (isGoDown)
            {
                deltaY = -moveDelta;
                deltaFramePosition = -delta;
            }
            else
            {
                deltaY = moveDelta;
                deltaFramePosition = delta;
            }

            Vector3 position = transform.localPosition;
            float deltapos = position.y + deltaY;

            while (WhileChecker(position.y, deltapos))
            {
                yield return new WaitForSecondsRealtime(moveTime);
                transform.localPosition += deltaFramePosition;
                position = transform.localPosition;
            }

            isGoDown = !isGoDown;
        }
    }
}
