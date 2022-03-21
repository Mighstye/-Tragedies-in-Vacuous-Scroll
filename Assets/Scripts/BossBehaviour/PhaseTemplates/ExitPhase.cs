using Game_Manager;
using Logic_System;
using UnityEngine;

namespace BossBehaviour
{
    public class ExitPhase : BossPhase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            LogicSystemAPI.instance.battleOutcome.RecordNewPhase(null);
            GameManagerAPI.instance.EndFight(true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

    }
}