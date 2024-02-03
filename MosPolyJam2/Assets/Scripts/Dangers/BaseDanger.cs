using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class BaseDanger : MonoBehaviour
{
    public UnityEvent OnComplete { get; private set; } = new();

    public string AnimationName;
    public abstract void Init();

    public virtual void Complete()
    {
        OnComplete?.Invoke();
    }
}