using UnityEngine;

public class SideFallingTarget : BaseTarget
{
    [Header("Key Activating Target")]
    [SerializeField] private Activation activatesOn;
    [Space(5)]
    [SerializeField] private KeyCode leftSideActivationKey = KeyCode.A;
    [SerializeField] private string leftSideAnimationName;
    [Space(5)]
    [SerializeField] private KeyCode rightSideActivationKey = KeyCode.D;
    [SerializeField] private string rightSideAnimationName;

    private void Update()
    {
        if (!CanInteract)
            return;

        if (Input.GetKeyDown(leftSideActivationKey))
        {
            if (!string.IsNullOrEmpty(leftSideAnimationName))
                animator.Play(leftSideAnimationName);

            Activate(activatesOn == Activation.Left || activatesOn == Activation.Both);
        }    
        else if (Input.GetKeyDown(rightSideActivationKey))
        {
            if (!string.IsNullOrEmpty(rightSideAnimationName))
                animator.Play(rightSideAnimationName);

            Activate(activatesOn == Activation.Right || activatesOn == Activation.Both);
        }
    }

    private enum Activation { Left, Right, Both };
}