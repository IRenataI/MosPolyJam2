using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseTarget : MonoBehaviour, IInteractable
{
    public bool IsActivated { get; protected set; }

    [Header("Activation")]
    public UnityEvent OnActivate = new();

    [SerializeField] protected bool canBeReused;
    [SerializeField] protected Animator animator;
    [SerializeField] protected string successActivationAnimationName;

    [Header("UI")]
    [TextArea] [SerializeField] protected string infoText;
    [SerializeField] protected GameObject infoUIPrefab;
    protected GameObject uiCanvas;
    protected GameObject infoUIInstance;

    [Header("Outline Settings")]
    [SerializeField] private Renderer rend;
    private Material[] objectMaterials;
    private Material[] changedMaterials;
    [SerializeField] private float defaultEmissiveness = 1f;
    [SerializeField] private float defaultOutlineOpacity = 0.5f;
    [SerializeField] private float targetEmissiveness = 1.2f;
    [SerializeField] private float targetOutlineOpacity = 0f;

    [Header("Refs")]
    [SerializeField] protected Transform targetObject;
    [SerializeField] protected float cameraHeight = 1f;

    protected Animator anim;
    protected bool CanInteract => !IsActivated || canBeReused; 

    private void Awake()
    {
        enabled = false;
        anim = GetComponent<Animator>();
        if (rend == null)
            TryGetComponent<Renderer>(out rend);

        objectMaterials = rend.sharedMaterials;

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

            rend.sharedMaterials = changedMaterials;
        }

        foreach(var mat in changedMaterials)
        {
            mat.SetFloat("_Emissiveness", _emissiveness);
            mat.SetFloat("_Outline_opacity", _outlineOpacity);
        }
    }

    public virtual void Init(GameObject uiCanvas)
    {
        if (uiCanvas == null || infoUIPrefab == null)
            return;

        this.uiCanvas = uiCanvas;

        infoUIInstance = Instantiate(infoUIPrefab, this.uiCanvas.transform);
        infoUIInstance.SetActive(false);
        infoUIInstance.GetComponentInChildren<TMP_Text>().text = infoText;
    }

    public void Select()
    {
        if (enabled)
            return;

        ChangeShaderSettings(targetEmissiveness, targetOutlineOpacity);
    }

    public void Deselect()
    {
        // Debug.Log($"{this.name} deselected");
        ChangeShaderSettings(defaultEmissiveness, defaultOutlineOpacity);
    }

    public void Interact(TargetSwitcher switcher)
    {
        switcher.SetTarget(this);
        switcher.SetTargetObject(targetObject != null ? targetObject : transform, cameraHeight * Vector3.up);
    }

    public BaseTarget GetTarget()
    {
        return this;
    }

    public void EnableUI(bool value)
    {
        if (infoUIInstance != null)
            infoUIInstance.SetActive(value);
    }

    public virtual void Activate(bool successActivation)
    {
        if (!CanInteract)
            return;

        Debug.Log($"{this.name} has been activated : {successActivation}");
        IsActivated = true;

        if (successActivation)
        {
            if (!string.IsNullOrEmpty(successActivationAnimationName))
                animator.Play(successActivationAnimationName);

            OnActivate?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((targetObject != null ? targetObject.position : transform.position) + cameraHeight * Vector3.up, 0.05f);
    }

    
}
