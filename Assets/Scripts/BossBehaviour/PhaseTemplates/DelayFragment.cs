using UnityEngine;

namespace BossBehaviour.PhaseTemplates
{
    public class DelayFragment: BossPhaseFragment
    {
        private static readonly int End = Animator.StringToHash("fragmentEndMove");
        [SerializeField] private float delayDuration;
        private float delayTimer;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(End, false);
            delayTimer = delayDuration;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0)
            {
                animator.SetBool(End,true);
            }
        }
        
        
    }
}