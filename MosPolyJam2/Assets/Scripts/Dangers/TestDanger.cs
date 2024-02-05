using TMPro;
using UnityEngine;

public class TestDanger : BaseDanger
{
    public override void Init(TimerView dangerTimerView)
    {
        base.Init(dangerTimerView);

        timer.StartTimer(dangerTime, () => Fail(), true);

        Debug.Log("Danger init");
    }
}