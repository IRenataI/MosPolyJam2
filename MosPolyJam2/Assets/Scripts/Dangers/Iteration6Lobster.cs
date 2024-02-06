using UnityEngine;

public class Iteration6Lobster : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Freeze()
    {
        animator.speed = 0f;
    }

    public void Unfreeze()
    {
        animator.speed = 1f;
    }

    public void DestroyAnimator()
    {
        Destroy(animator);
    }
}