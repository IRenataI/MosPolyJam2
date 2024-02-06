using UnityEngine;

public class Iteration6Knife : MonoBehaviour
{
    [SerializeField] private DangerIteration6 di6;

    public void OnHitLobster()
    {
        di6.PutDangerousIntoKnife();
    }
}