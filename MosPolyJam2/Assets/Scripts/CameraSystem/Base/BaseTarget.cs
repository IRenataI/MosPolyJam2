using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseTarget : MonoBehaviour, IInteractable
{
    public bool IsEnabled { get; set; } = false;
    public bool IsActivated { get; protected set; }
    public UnityEvent OnActivate { get; private set; } = new();

    [Header("Activation")]
    [SerializeField] protected bool canBeReused;
    [SerializeField] protected Animator animator;
    [SerializeField] protected string activationAnimationName;
    
    [Header("Outline Settings")]
    private Material[] objectMaterials;
    private Material[] changedMaterials;
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
        objectMaterials = targetObject.GetComponent<Renderer>().sharedMaterials;

        // set outline
        ChangeShaderSettings(defaultEmissiveness, defaultOutlineOpacity);

    }

    protected void ChangeShaderSettings(float _emissiveness, float _outlineOpacity)
    {
        if(objectMaterials.Length <= 0)
        {
            Debug.LogWarning($"No material in base target {this.name}");
            return;
        }

        if(changedMaterials == null)
        {
            changedMaterials = new Material[objectMaterials.Length];
            for (int i = 0; i < changedMaterials.Length; i++)
            {
                changedMaterials[i] = new Material(objectMaterials[i]);
            }
            targetObject.GetComponent<Renderer>().sharedMaterials = changedMaterials;
        }

        foreach(var mat in changedMaterials)
        {
            mat.SetFloat("_Emissiveness", _emissiveness);
            mat.SetFloat("_Outline_opacity", _outlineOpacity);
        }
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
        if (IsEnabled)
            return;

        Debug.Log($"{this.name} selected");
        ChangeShaderSettings(targetEmissiveness, targetOutlineOpacity);
    }

    public BaseTarget GetTarget()
    {
        return this;
    }

    public virtual void Activate()
    {
        if (!canBeReused && IsActivated)
            return;

        if (!string.IsNullOrEmpty(activationAnimationName))
            animator.Play(activationAnimationName);

        IsActivated = true;
        OnActivate?.Invoke();
        Debug.Log($"{this.name} has been activated");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((targetObject != null ? targetObject.position : transform.position) + cameraHeight * Vector3.up, 0.05f);
    }

    
}
