using UnityEngine;
using UnityEngine.Events;

public class OlegTutorialAnimationsTriggers : MonoBehaviour
{
    private Animator anim;
    public UnityEvent OnFreeze;
    public UnityEvent OnUnFreeze;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Freeze()
    {
        anim.speed = 0f;
        OnFreeze?.Invoke();
    }

    public void UnFreeze()
    {
        anim.speed = 1f;
        OnUnFreeze?.Invoke();
    }
}
