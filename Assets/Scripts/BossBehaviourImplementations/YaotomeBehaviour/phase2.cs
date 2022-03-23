using UnityEngine;

public class phase2 : BossPhase
{
    protected override void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Phase 2 started");
    }
}