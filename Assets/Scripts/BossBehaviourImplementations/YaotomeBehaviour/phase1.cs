using System.Collections;
using System.Collections.Generic;
using BossBehaviour;
using UnityEngine;

public class phase1 : BossPhase
{
    protected override void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Phase 1 started");
    }
}
