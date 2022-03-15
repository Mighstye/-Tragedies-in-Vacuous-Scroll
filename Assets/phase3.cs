using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;

public class phase3 : BossPhase // Only to create Win Menu ect... To be deleted
{
    protected override void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Phase 3 started - End of the fight");
        GameManagerAPI.instance.EndFight(true);
    }
}
