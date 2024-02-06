using UnityEngine;

public class KeyActivatingTarget : BaseTarget
{
    [Header("Key Activating Target")]
    [SerializeField] private KeyCode activationKey = KeyCode.Q;
    [SerializeField] private int neededPressCount = 1;
    [SerializeField] private string pressAnimationName;

    private int pressCount;

    private void Update()
    {
        if (!CanInteract || !Input.GetKeyDown(activationKey))
            return;

        pressCount++;
        if (!string.IsNullOrEmpty(pressAnimationName))
            animator.Play(pressAnimationName);

        if (pressCount >= neededPressCount)
        {
            Activate(true);
        }
    }
}