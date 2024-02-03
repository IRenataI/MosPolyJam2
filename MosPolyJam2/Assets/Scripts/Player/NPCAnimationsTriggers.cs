using UnityEngine;

public class NPCAnimationsTriggers : MonoBehaviour
{
    [SerializeField] private NonPlayableCharacter npc;
    [SerializeField] private ProgressManager progressManager;
    public void Freeze()
    {
        npc.Freeze();
    }

    public void Continue()
    {
        progressManager.MoveToNextProgressPoint();
    }
}
