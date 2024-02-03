using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IInteractable
{
    [Header("Activation settings")]
    public KeyCode activationKey;
    protected bool isActive;

    [Header("Refs")]
    [SerializeField] protected Transform targetObject;

    private void Start()
    {
        // set outline
    }

    public void Deselect()
    {
        Debug.Log($"{this.name} deselected");
        // change outline
    }

    public void Interact(TargetSwitcher switcher)
    {
        switcher.SetTargetObject(targetObject, this);
    }

    public void Select()
    {
        Debug.Log($"{this.name} selected");
        // change outline
    }

    public virtual IEnumerator Activate()
    {
        Debug.Log($"{this.name} has been activated");
        yield return new WaitForEndOfFrame();
    }
}
