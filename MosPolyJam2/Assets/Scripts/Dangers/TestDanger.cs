using TMPro;
using UnityEngine;

public class TestDanger : BaseDanger
{
    public override void Init(TextMeshProUGUI dangerTimerLabel)
    {
        base.Init(dangerTimerLabel);

        timer.StartTimer(dangerTime, () => Complete(), true);

        Debug.Log("Danger init");
    }
}