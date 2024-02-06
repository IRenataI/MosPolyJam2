using UnityEngine;
using UnityEngine.Events;

public abstract class BaseDanger : MonoBehaviour
{
    public bool IsCompleted { get; private set; }
    public UnityEvent<bool> OnComplete { get; private set; } = new();

    public string NPCAnimationName => npcAnimationName;

    [SerializeField] protected string npcAnimationName;
    [SerializeField, Min(2.5f)] protected float time = 30f;
    [SerializeField] protected GameObject uiPrefab;

    [Header("Targets\nAdd target if it affects on danger or has user interface")]
    [Space(5)]
    [SerializeField] private BaseTarget[] completeTargets;
    [SerializeField] private BaseTarget[] failTargets;
    [SerializeField] private BaseTarget[] nonImpactTargets;

    protected TimerView dangerTimerView;
    protected GameObject uiPanel;

    protected Timer timer;

    protected void InitTargets()
    {
        foreach (BaseTarget target in completeTargets)
        {
            target.OnActivate.AddListener(Success);
            target.Init(uiPanel);
        }
        foreach (BaseTarget target in failTargets)
        {
            target.OnActivate.AddListener(Fail);
            target.Init(uiPanel);
        }
        foreach (BaseTarget target in nonImpactTargets)
        {
            target.Init(uiPanel);
        }
    }

    protected virtual void Complete()
    {
        IsCompleted = true;

        timer.Destroy();

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

        Debug.Log("Danger init: " + name);

        this.dangerTimerView = dangerTimerView;

        if (uiPrefab != null)
            uiPanel = Instantiate(uiPrefab);

        timer = gameObject.AddComponent<Timer>();
        timer.TimeChanged.AddListener(OnTimerTick);
        timer.StartTimer(time, () => Fail(), true);

        InitTargets();
    }

    public virtual void OnTimerTick(float remainingTime)
    {
        if (dangerTimerView != null)
            dangerTimerView.SetTime(remainingTime);
    }
}