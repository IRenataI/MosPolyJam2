using UnityEngine;

public class DangerIteration6 : BaseDanger
{
    [Header("Danger Iteration 6")]
    [SerializeField] private Transform knife;
    [SerializeField] private Transform lobster;

    [Header("NPC")]
    [SerializeField] private NonPlayableCharacter npc;
    [SerializeField] private string npcFailureAnimationName;

    public override void Init(TimerView dangerTimerView)
    {
        base.Init(dangerTimerView);

        lobster.GetComponent<Animator>().Play("Lobster");
    }

    protected override void Success()
    {
        base.Success();

        NPCAnimationsTriggers npcAnims = npc.GetComponentInChildren<NPCAnimationsTriggers>();
        npcAnims.Continue();

        Iteration6Lobster lob6 = lobster.GetComponent<Iteration6Lobster>();
        lob6.DestroyAnimator();
    }

    protected override void Fail()
    {
        base.Fail();

        lobster.GetComponent<Iteration6Lobster>().Unfreeze();
        npc.PlayAnimation(npcFailureAnimationName);
    }

    public void PutDangerousIntoKnife()
    {
        lobster.parent = knife;
    }
}