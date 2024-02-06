using UnityEngine;

public class TestDanger : BaseDanger
{
    public override void Init(TimerView dangerTimerView)
    {
        base.Init(dangerTimerView);

        Debug.Log("Danger init");
    }
}