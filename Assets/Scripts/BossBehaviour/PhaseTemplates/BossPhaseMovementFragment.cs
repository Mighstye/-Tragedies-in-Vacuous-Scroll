using BulletSystem;
using DG.Tweening;
using UnityEngine;

namespace BossBehaviour
{
    public abstract class BossPhaseMovementFragment : BossPhaseFragment
    {
        public UpdateMethod updateMethod;
        protected BossController bossController;
        private static readonly int End = Animator.StringToHash("fragmentEndMove");
        private bool endFlag = false;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            endFlag = false;
            animator.SetBool(End,false);
            bossController = BossBehaviourSystemProxy.instance.bossController;
            if (updateMethod is UpdateMethod.Classic)
            {
                bossController.bossMotion = BossMovementUpdate;
            }
            else
            {
                var sequence = DOTween.Sequence();
                sequence.OnComplete(() => endFlag = true);
                BossTween(sequence);
            }
            CustomFragmentStart();
        }

        protected virtual void CustomFragmentStart()
        {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            bossController.bossMotion = null;
            CustomFragmentEnd();
        }

        protected virtual void CustomFragmentEnd()
        {
            
        }

        protected virtual void BossMovementUpdate()
        {
            
        }

        protected virtual void BossTween(Sequence sequence)
        {
            
        }

        protected virtual bool FragmentEnd()
        {
            return false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (endFlag && updateMethod is UpdateMethod.Classic) return;
            if ((!FragmentEnd() || updateMethod is not UpdateMethod.Classic) &&
                (!endFlag || updateMethod is not UpdateMethod.DoTween)) return;
            animator.SetBool(End,true);
            endFlag = true;
        }
        
    }
}