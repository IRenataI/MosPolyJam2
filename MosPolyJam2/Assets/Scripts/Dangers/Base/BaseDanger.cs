using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseDanger : MonoBehaviour
{
    public bool IsCompleted { get; private set; }
    public UnityEvent<bool> OnComplete { get; private set; } = new();

    public string AnimationName => animationName;

    [SerializeField] protected string animationName;
    [SerializeField, Min(2.5f)] protected float dangerTime = 2.5f;
    [SerializeField] protected GameObject uiPrefab;

    [SerializeField] private BaseTarget[] completeTargets;
    [SerializeField] private BaseTarget[] failTargets;
    [SerializeField] private BaseTarget[] nonImpactTargets;

    protected TimerView dangerTimerView;
    protected GameObject uiPanel;

    protected Timer timer;

    protected void InitTargets()
    {
        foreach (BaseTarget target in completeTargets)
            target.OnActivate.AddListener(Success);
        foreach (BaseTarget target in failTargets)
            target.OnActivate.AddListener(Fail);
    }

    protected virtual void Complete()
    {
        IsCompleted = true;

        if (uiPanel != null)
            Destroy(uiPanel);
    }

    protected virtual void Success()
    {
        if (IsCompleted)
            return;

        Debug.Log("Complete");

        Complete();
        OnComplete?.Invoke(true);
    }

    protected virtual void Fail()
    {
        if (IsCompleted)
            return;

        Debug.Log("Fail");

        Complete();
        OnComplete?.Invoke(false);
    }

    public virtual void Init(TimerView dangerTimerView)
    {
        if (IsCompleted)
        {
            Debug.LogWarning("The danger is already over");
            return;
        }

        this.dangerTimerView = dangerTimerView;

        if (uiPrefab != null)
            uiPanel = Instantiate(uiPrefab);

        timer = gameObject.AddComponent<Timer>();
        timer.TimeChanged.AddListener(OnTimerTick);

        InitTargets();
    }

    public virtual void OnTimerTick(float remainingTime)
    {
        if (dangerTimerView != null)
            dangerTimerView.SetTime(remainingTime);
    }
}