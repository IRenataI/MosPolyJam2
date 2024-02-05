using UnityEngine;

public class SideFallingTarget : BaseTarget
{
    [Header("Key Activating Target")]
    [SerializeField] private KeyCode leftSideActivationKey = KeyCode.A;
    [SerializeField] private KeyCode rightSideActivationKey = KeyCode.D;
    [SerializeField] private string leftSideAnimationName;
    [SerializeField] private string rightSideAnimationName;

    private void Update()
    {
        if (!IsEnabled)
            return;

        if (Input.GetKeyDown(leftSideActivationKey))
        {
            animator.Play(leftSideAnimationName);
        }
        else if (Input.GetKeyDown(leftSideActivationKey))
        {
            animator.Play(rightSideAnimationName);
        }

        Activate();
    }
}