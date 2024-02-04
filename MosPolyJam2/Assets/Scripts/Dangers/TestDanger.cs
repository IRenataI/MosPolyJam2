using UnityEngine;

public class TestDanger : BaseDanger
{
    public override void Init()
    {
        base.Init();

        timer.StartTimer(dangerTimer, () => Complete(), true);

        Debug.Log("Danger init");
    }
}