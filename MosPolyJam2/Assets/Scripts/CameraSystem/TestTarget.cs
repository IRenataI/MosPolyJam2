using UnityEngine;

public class TestTarget : BaseTarget
{
    public override void Activate()
    {
        base.Activate();

        Instantiate(ActivationVFX, this.transform);
    }
}
