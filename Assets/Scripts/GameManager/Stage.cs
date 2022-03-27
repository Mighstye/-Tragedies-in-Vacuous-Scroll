using BossBehaviour;
using UnityEngine;

namespace GameManager
{
    public class Stage : StateMachineBehaviour
    {
        
        [SerializeField] private BossAsset bossAsset;
        private static readonly int StageSelect = Animator.StringToHash("stageSelect");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger(StageSelect,0);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            
        }
        
        
    }
}