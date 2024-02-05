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
        if (!IsEnabled || !Input.GetKeyDown(activationKey))
            return;

        pressCount++;
        animator.Play(pressAnimationName);

        if (pressCount >= neededPressCount)
        {
            Activate();
        }
    }
}