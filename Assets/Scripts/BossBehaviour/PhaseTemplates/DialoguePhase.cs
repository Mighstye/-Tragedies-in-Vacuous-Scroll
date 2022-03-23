using DialogueSystem;
using Logic_System;
using UnityEngine;
using UnityEngine.Localization;

namespace BossBehaviour
{
    public class DialoguePhase : StateMachineBehaviour
    {
        private static readonly int PhaseFlowSelectedChoice = Animator.StringToHash("selectedChoice");
        [SerializeField] private string storyKey;
        [SerializeField] private LocalizedAsset<TextAsset> localizedAsset;
        [SerializeField] private TextAsset storyAsset;
        [SerializeField] private bool storyOverwrite = true;
        private BattleOutcome battleOutcome;
        private DialogueSystemManager dialogueSystemManagerRef;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            dialogueSystemManagerRef = DialogueSystemManager.instance;
            animator.SetInteger(PhaseFlowSelectedChoice, -1);
            battleOutcome = LogicSystemAPI.instance.battleOutcome;
            battleOutcome.RecordNewPhase(null);
            if (!storyOverwrite) return;
            localizedAsset.TableReference = "Stories";
            localizedAsset.TableEntryReference = storyKey;
            dialogueSystemManagerRef.Init(localizedAsset);
        }


        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger(PhaseFlowSelectedChoice, -1);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}