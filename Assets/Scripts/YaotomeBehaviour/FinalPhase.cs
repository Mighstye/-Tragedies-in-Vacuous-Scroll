using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPhase : BossPhase
{
    protected override void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Final Phase started");
    }
}
