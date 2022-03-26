using Game_Manager;
using Logic_System;
using UnityEngine;
using Utils;

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