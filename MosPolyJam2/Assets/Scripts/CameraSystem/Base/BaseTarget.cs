using UnityEngine;
using UnityEngine.Events;

public abstract class BaseTarget : MonoBehaviour, IInteractable
{
    public bool IsActiveted { get; private set; }
    public UnityEvent OnActivate { get; private set; } = new();

    [Header("Activation settings")]
    public KeyCode activationKey;

    [Header("Refs")]
    [SerializeField] protected Transform targetObject;
    [SerializeField] protected GameObject ActivationVFX;
    [SerializeField] protected float cameraHeight = 1f;

    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        // set outline
    }

    public void Deselect()
    {
        Debug.Log($"{this.name} deselected");
        // change outline
    }

    public void Interact(TargetSwitcher switcher)
    {
        switcher.SetTargetObject(targetObject != null ? targetObject : transform, this, cameraHeight * Vector3.up);
    }

    public void Select()
    {
        Debug.Log($"{this.name} selected");
        // change outline
    }

    public virtual void Activate()
    {
        IsActiveted = true;
        OnActivate?.Invoke();

        Debug.Log($"{this.name} has been activated");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + cameraHeight * Vector3.up, 0.05f);
    }
}
