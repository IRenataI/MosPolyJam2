using UnityEngine;

public class TestDanger : BaseDanger
{
    public override void Init()
    {
        base.Init();

        timer.StartTimer(5f, () => Complete(), true);

        Debug.Log("Danger init");
    }
}