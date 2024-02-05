public interface IInteractable
{
    public void Select();
    public void Interact(TargetSwitcher switcher);
    public void Deselect();
    public BaseTarget GetTarget();
}
