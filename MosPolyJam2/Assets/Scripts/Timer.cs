using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent<float> TimeChanged { get; private set; } = new();

    private Action expired;
    private float time;
    private bool isStopped = true;
    private bool destroyOnExpired;

    private bool IsExpired => time <= 0;

    private void Update()
    {
        if (isStopped)
            return;

        time = Mathf.Max(time - Time.deltaTime, 0);
        TimeChanged.Invoke(time);

        if (IsExpired)
        {
            OnExpired();
        }
    }

    private void OnExpired()
    {
        Debug.Log("Timer Expired");
        isStopped = true;
        expired?.Invoke();

        if (destroyOnExpired)
            Destroy(this);
    }

    public void StartTimer(float time, Action expired, bool destroyOnExpired = true)
    {
        if (time <= 0f)
        {
            OnExpired();
            return;
        }

        this.time = time;
        this.expired = expired;
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