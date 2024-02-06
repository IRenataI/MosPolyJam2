using UnityEngine;

public class DangerIteration5 : BaseDanger
{
    [SerializeField] private NonPlayableCharacter npc;
    [SerializeField] private string npcFailureAnimationName;

    protected override void Fail()
    {
        base.Fail();

        npc.PlayAnimation(npcFailureAnimationName);
    }
}