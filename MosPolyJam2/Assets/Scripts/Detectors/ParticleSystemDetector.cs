using UnityEngine;

public class ParticleSystemDetector : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;

    public void PlayParticleSystem()
    {
        foreach(var e in particleSystems)
        {
            e.Play();
        }
    }

    public void PauseParticleSystem()
    {
        foreach (var e in particleSystems)
        {
            e.Pause();
        }
    }
}
