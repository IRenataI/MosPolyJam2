using Unity.VisualScripting;
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

    private GameObject uiInstance;

    protected Timer timer;

    public virtual void Init()
    {
        if (uiPrefab != null)
            uiInstance = Instantiate(uiPrefab);

        timer = gameObject.AddComponent<Timer>();
    }

    public virtual void Complete()
    {
        if (IsCompleted)
            return;

        if (uiPrefab != null)
            Destroy(uiInstance);

        IsCompleted = true;

        OnComplete?.Invoke();
    }
}