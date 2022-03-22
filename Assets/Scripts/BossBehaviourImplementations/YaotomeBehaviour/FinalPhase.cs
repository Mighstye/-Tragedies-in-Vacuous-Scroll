using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;

public class FinalPhase : BossPhase
{
    protected override void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Final Phase started");
    }

    protected override void OnPhaseEndCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
