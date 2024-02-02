using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IInteractable
{
    [Header("Activation settings")]
    public KeyCode activationKey;
    protected bool isActive;

    [Header("Refs")]
    [SerializeField] protected Transform targetObject;

    public void Deselect()
    {
        Debug.Log($"{this.name} deselected");
    }

    public void Interact(TargetSwitcher switcher)
    {
        switcher.SetTargetObject(targetObject, this);
    }

    public void Select()
    {
        Debug.Log($"{this.name} selected");
    }

    public virtual IEnumerator Activate()
    {
        Debug.Log($"{this.name} has been activated");
        yield return new WaitForEndOfFrame();
    }
}
