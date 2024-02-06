using UnityEngine;

public class AnimatorDetector : MonoBehaviour
{
    [SerializeField] private Animator[] anims;

    public void PlayAnimators()
    {
        foreach (var e in anims)
        {
            e.speed = 1f;
        }
    }

    public void PauseAnimators()
    {
        foreach (var e in anims)
        {
            e.speed = 0f;
        }
    }
}
