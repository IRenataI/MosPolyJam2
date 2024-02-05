using UnityEngine;

public class TestTarget : BaseTarget
{
    public override void Activate()
    {
        base.Activate();

        Instantiate(ActivationVFX, this.transform);
    }

    private void Update()
    {
        if (!IsEnabled)
            return;

        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            Activate();
        }
    }
}
