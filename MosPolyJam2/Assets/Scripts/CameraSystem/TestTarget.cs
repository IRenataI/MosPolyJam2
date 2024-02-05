using UnityEngine;

public class TestTarget : BaseTarget
{
    public override void Activate(bool successActivation)
    {
        base.Activate(true);

        if(ActivationVFX != null)
            Instantiate(ActivationVFX, this.transform);
    }

    private void Update()
    {
        if (!CanInteract)
            return;

        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            Activate(true);
        }
    }
}
