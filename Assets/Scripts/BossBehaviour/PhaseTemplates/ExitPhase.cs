using BossBehaviour.PhaseTemplates;
using GameManager;
using Logic_System;
using UnityEngine;
using Utils;
using Utils.Events;

namespace BossBehaviour
{
    public class ExitPhase : BossPhase
    {
        [SerializeField] private GameEvent onBossDefeat;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            LogicSystemAPI.instance.battleOutcome.RecordNewPhase(null);
            onBossDefeat.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}