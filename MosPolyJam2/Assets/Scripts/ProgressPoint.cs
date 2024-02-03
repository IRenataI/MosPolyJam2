[System.Serializable]
public struct ProgressPoint
{
    public UnityEngine.Transform worldPositionTransform;
    public bool hasEvent;
    public UnityEngine.Events.UnityEvent onReached;
}