using System;
using System.Collections;
using UnityEngine;

public class MoneyArrow : MonoBehaviour
{
    [SerializeField] float rotateTime;

    Quaternion startRotation;
    float angle;
    bool isRight;

    void Start()
    {
        startRotation = transform.rotation;
        isRight = true;
        StartCoroutine(ChangeAngle());
    }

    bool IsFinishAngle(bool isRight)
    {
        if (isRight)
        {
            if (angle < 180) return true;
            else return false;
        }
        else
        {
            if (angle > 0) return true;
            else return false;
        }
    }

    public void Stop() => StopAllCoroutines();

    IEnumerator ChangeAngle()
    {
        Vector3 rotate = Vector3.back;
        int i;

        while (true)
        {
            if (isRight) i = 1;
            else i = -1;

            while(IsFinishAngle(isRight))
            {
                yield return new WaitForSecondsRealtime(rotateTime);
                Quaternion rotationY = Quaternion.AngleAxis(angle, rotate);
                transform.rotation = startRotation * rotationY;
                angle += i;
            }

            isRight = !isRight;
        }
    }

}
