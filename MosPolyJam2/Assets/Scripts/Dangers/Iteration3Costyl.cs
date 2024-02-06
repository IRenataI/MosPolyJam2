using UnityEngine;

public class Iteration3Costyl : MonoBehaviour
{
    [SerializeField] private DangerIteration3 di3;

    public void Parenting()
    {
        di3.PutDangerousIntoHand();
    }

    public void Unparenting()
    {
        di3.UnputDangerousIntoHand();
    }
}