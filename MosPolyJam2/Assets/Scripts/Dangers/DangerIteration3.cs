using UnityEngine;

public class DangerIteration3 : BaseDanger
{
    [Header("Danger Iteration 3")]
    [SerializeField] private Transform dangerousTransform;
    [SerializeField] private Transform handTransform;

    [SerializeField] private GameObject activationVFX;
    [SerializeField] private GameObject looseVFX;

    private Vector3 position;
    private Vector3 lossyScale;
    private Quaternion rotation;

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

        position = dangerousTransform.position;
        rotation = dangerousTransform.rotation;
        lossyScale = dangerousTransform.lossyScale;

        dangerousTransform.localPosition = Vector3.zero;
        dangerousTransform.rotation = Quaternion.identity;
    }

    public void UnputDangerousIntoHand()
    {
        dangerousTransform.parent = transform;

        dangerousTransform.position = position;
        dangerousTransform.rotation = rotation;
        dangerousTransform.localScale = lossyScale;
    }
}