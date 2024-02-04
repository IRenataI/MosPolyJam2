using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseDanger : MonoBehaviour
{
    public bool IsCompleted { get; private set; }
    public UnityEvent OnComplete { get; private set; } = new();

    public string AnimationName => animationName;

    [SerializeField] protected string animationName;
    [SerializeField] protected float dangerTime;
    [SerializeField] protected GameObject uiPrefab;

    [SerializeField] private BaseTarget[] completeTargets;
    [SerializeField] private BaseTarget[] failTargets;
    [SerializeField] private BaseTarget[] nonImpactTargets;

    protected TextMeshProUGUI dangerTimerLabel;
    protected GameObject uiPanel;

    protected Timer timer;

    protected void InitTargets()
    {
        foreach (BaseTarget target in completeTargets)
            target.OnActivate.AddListener(Complete);
        foreach (BaseTarget target in failTargets)
            target.OnActivate.AddListener(Fail);
    }

    public virtual void Init(TextMeshProUGUI dangerTimerLabel)
    {
        this.dangerTimerLabel = dangerTimerLabel;

        if (uiPrefab != null)
            uiPanel = Instantiate(uiPrefab);

        timer = gameObject.AddComponent<Timer>();
        timer.TimeChanged.AddListener(OnTimerTick);

        InitTargets();
    }

    public virtual void Complete()
    {
        if (IsCompleted)
            return;

        Debug.Log("Complete");

        if (uiPanel != null)
            Destroy(uiPanel);

        IsCompleted = true;

        OnComplete?.Invoke();
    }

    public virtual void Fail()
    {
        Debug.Log("Fail");
    }

    public virtual void OnTimerTick(float remainingTime)
    {
        if (dangerTimerLabel != null)
            dangerTimerLabel.text = ((int)remainingTime).ToString();
    }
}