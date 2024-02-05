using UnityEngine;
using UnityEngine.Events;

public abstract class BaseTarget : MonoBehaviour, IInteractable
{
    public bool IsEnabled { get; set; } = false;
    public UnityEvent OnActivate { get; private set; } = new();

    [Header("Activation settings")]
    public KeyCode activationKey;

    [Header("Outline Settings")]
    private Material objectMaterial;
    [SerializeField] private float defaultEmissiveness = 1f;
    [SerializeField] private float defaultOutlineOpacity = 1f;
    [SerializeField] private float targetEmissiveness = 1.2f;
    [SerializeField] private float targetOutlineOpacity = 0f;

    [Header("Refs")]
    [SerializeField] protected Transform targetObject;
    [SerializeField] protected GameObject ActivationVFX;
    [SerializeField] protected float cameraHeight = 1f;

    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        objectMaterial = targetObject.GetComponent<Renderer>().sharedMaterial;

        // set outline
        ChangeShaderSettings(defaultEmissiveness, defaultOutlineOpacity);

    }

    protected void ChangeShaderSettings(float _emissiveness, float _outlineOpacity)
    {
        if(objectMaterial == null)
        {
            Debug.LogWarning($"No material in base target {this.name}");
            return;
        }

        objectMaterial.SetFloat("_Emissiveness", _emissiveness);
        objectMaterial.SetFloat("_Outline_opacity", _outlineOpacity);
    }

    public void Deselect()
    {
        Debug.Log($"{this.name} deselected");
        ChangeShaderSettings(defaultEmissiveness, defaultOutlineOpacity);
    }

    public void Interact(TargetSwitcher switcher)
    {
        switcher.SetTarget(this);
        switcher.SetTargetObject(targetObject != null ? targetObject : transform, cameraHeight * Vector3.up);
    }

    public void Select()
    {
        Debug.Log($"{this.name} selected");
        ChangeShaderSettings(targetEmissiveness, targetOutlineOpacity);
    }

    public virtual void Activate()
    {
        OnActivate?.Invoke();
        Debug.Log($"{this.name} has been activated");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + cameraHeight * Vector3.up, 0.05f);
    }
}
