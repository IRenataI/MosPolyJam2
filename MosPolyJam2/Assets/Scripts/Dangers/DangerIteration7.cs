using UnityEngine;

public class DangerIteration7 : BaseDanger
{
    [Header("Danger Iteration 7")]

    [SerializeField] private BaseTarget[] targets;

    private int targetsCount = 0;

    private void Start()
    {
        foreach(var target in targets)
        {
            target.OnActivate.AddListener(AddTagetsCount);
        }
    }

    private void AddTagetsCount()
    {
        targetsCount++;
        if(targetsCount >= targets.Length)
        {
            targetsCount = 0;
            Success();
        }
    }
}
