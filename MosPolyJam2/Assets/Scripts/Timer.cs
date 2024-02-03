using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private UnityEvent expired;
    private float time;
    private bool isStopped;
    private bool destroyOnExpired;

    private bool IsExpired => time <= 0;

    private void Update()
    {
        if (isStopped)
            return;

        time -= Time.deltaTime;

        if (IsExpired)
        {
            OnExpired();
        }
    }

    private void OnExpired()
    {
        isStopped = true;
        expired.Invoke();

        if (destroyOnExpired)
            Destroy(this);
    }

    public void StartTimer(float time, Action expired, bool destroyOnExpired = true)
    {
        if (IsExpired)
        {
            OnExpired();
            return;
        }

        this.expired = (UnityEvent)expired.Target;
        this.time = time;
        this.destroyOnExpired = destroyOnExpired;
        isStopped = false;
    }

    public void Continue()
    {
        isStopped = false;
    }

    public void Stop()
    {
        isStopped = true;
    }
}