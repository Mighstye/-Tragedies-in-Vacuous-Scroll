using DialogueSystem;
using Logic_System;
using UnityEngine;

namespace BossBehaviour
{
    public class DialoguePhase : StateMachineBehaviour
    {
        private DialogueSystemManager dialogueSystemManagerRef;
        private BattleOutcome battleOutcome;
        [SerializeField] private TextAsset storyAsset;
        [SerializeField] private bool storyOverwrite = true;
        private static readonly int PhaseFlowSelectedChoice = Animator.StringToHash("selectedChoice");
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            dialogueSystemManagerRef = DialogueSystemManager.instance;
            if(storyOverwrite) dialogueSystemManagerRef.Init(storyAsset);
            animator.SetInteger(PhaseFlowSelectedChoice,-1);
            battleOutcome = LogicSystemAPI.instance.battleOutcome;
            battleOutcome.RecordNewPhase(null);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger(PhaseFlowSelectedChoice,-1);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
        
    }
}