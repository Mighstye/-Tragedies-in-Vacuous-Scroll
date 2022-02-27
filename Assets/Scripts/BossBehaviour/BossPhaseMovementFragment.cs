using UnityEngine;

namespace BossBehaviour
{
    public abstract class BossPhaseMovementFragment : StateMachineBehaviour
    {
        protected BossController bossController;
        private static readonly int End = Animator.StringToHash("fragmentEndMove");
        private bool endFlag = false;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            endFlag = false;
            animator.SetBool(End,false);
            bossController = BossController.instance;
            bossController.bossMotion = BossMovementUpdate;
            CustomFragmentStart();
        }

        protected virtual void CustomFragmentStart()
        {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            CustomFragmentEnd();
        }

        protected virtual void CustomFragmentEnd()
        {
            
        }

        protected virtual void BossMovementUpdate()
        {
            
        }

        protected virtual bool FragmentEnd()
        {
            return false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (endFlag) return;
            if (!FragmentEnd()) return;
            animator.SetBool(End,true);
            endFlag = true;
        }
        
    }
}