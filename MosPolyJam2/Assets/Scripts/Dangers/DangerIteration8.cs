using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerIteration8 : BaseDanger
{
    [Header("Danger Iteration 8")]
    [SerializeField] private string loseAnimationName;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Fail()
    {
        if (IsCompleted)
            return;

        Debug.Log("Fail");

        anim.Play(loseAnimationName);

        Complete();
        OnComplete?.Invoke(false);
    }
}
