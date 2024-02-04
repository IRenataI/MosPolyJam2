using UnityEngine;
using UnityEngine.Events;

public abstract class BaseDanger : MonoBehaviour
{
    public bool IsCompleted { get; private set; }
    public UnityEvent OnComplete { get; private set; } = new();

    public string AnimationName => animationName;

    [SerializeField] protected string animationName;
    [SerializeField] protected float dangerTimer;
    [SerializeField] protected GameObject uiPrefab;
    [SerializeField] private BaseTarget[] completeTargets;
    [SerializeField] private BaseTarget[] failTargets;

    protected GameObject uiInstance;
    protected Timer timer;

    protected void InitTargets()
    {
        foreach (BaseTarget target in completeTargets)
            target.OnActivate.AddListener(Complete);
        foreach (BaseTarget target in failTargets)
            target.OnActivate.AddListener(Fail);
    }

    public virtual void Init()
    {
        if (uiPrefab != null)
            uiInstance = Instantiate(uiPrefab);

        timer = gameObject.AddComponent<Timer>();

        InitTargets();
    }

    public virtual void Complete()
    {
        if (IsCompleted)
            return;

        Debug.Log("Complete");

        if (uiPrefab != null)
            Destroy(uiInstance);

        IsCompleted = true;

        OnComplete?.Invoke();
    }

    public virtual void Fail()
    {
        Debug.Log("Fail");
    }
}