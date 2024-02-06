using UnityEngine;

public class NPCAnimationsTriggers : MonoBehaviour
{
    [SerializeField] private NonPlayableCharacter npc;
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private ParticleSystemDetector particleSystemDetector;
    [SerializeField] private AnimatorDetector animatorDetector;

    public void Freeze()
    {
        npc.Freeze();
        particleSystemDetector.PauseParticleSystem();
        animatorDetector.PauseAnimators();
    }

    public void Continue()
    {
        progressManager.MoveToNextProgressPoint();
        particleSystemDetector.PlayParticleSystem();
        animatorDetector.PlayAnimators();
    }
}
