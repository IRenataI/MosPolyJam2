[System.Serializable]
public struct ProgressPoint
{
    public UnityEngine.Transform worldPositionTransform;
    public BaseDanger dangerAction;
    public UnityEngine.Events.UnityEvent onReached;
}