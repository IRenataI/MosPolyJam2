using UnityEngine;

public class DangerIteration1 : BaseDanger
{
    [SerializeField] private ParticleSystem startVFX;
    [SerializeField] private GameObject loseVFX;

    public override void Init(TimerView dangerTimerView)
    {
        base.Init(dangerTimerView);

        timer.StartTimer(time, () => Fail(), true);

        startVFX.Play();

        Debug.Log("Danger init");
    }

    protected override void Complete()
    {
        base.Complete();
    }

    protected override void Fail()
    {
        base.Fail();

        loseVFX.SetActive(true);
    }

    protected override void Success()
    {
        base.Success();

        startVFX.Stop();
    }
}
