using UnityEngine;

public class DangerIteration3 : BaseDanger
{
    [Header("Danger Iteration 3")]
    [SerializeField] private Transform dangerousTransform;
    [SerializeField] private Transform handTransform;

    [SerializeField] private GameObject activationVFX;
    [SerializeField] private GameObject looseVFX;

    public override void Init(TimerView dangerTimerView)
    {
        base.Init(dangerTimerView);

        activationVFX.SetActive(true);
    }

    protected override void Success()
    {
        base.Success();

        activationVFX.SetActive(false);
    }

    protected override void Fail()
    {
        base.Fail();

        looseVFX.SetActive(true);
    }

    public void PutDangerousIntoHand()
    {
        dangerousTransform.parent = handTransform;

        dangerousTransform.localPosition = Vector3.zero;
        dangerousTransform.rotation = Quaternion.identity;
    }
}