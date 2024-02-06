using UnityEngine;

public class DangerIteration3 : BaseDanger
{
    [Header("Danger Iteration 3")]
    [SerializeField] private Transform dangerousTransform;
    [SerializeField] private Transform handTransform;

    public void PutDangerousIntoHand()
    {
        dangerousTransform.parent = handTransform;

        dangerousTransform.localPosition = Vector3.zero;
        dangerousTransform.rotation = Quaternion.identity;
    }
}