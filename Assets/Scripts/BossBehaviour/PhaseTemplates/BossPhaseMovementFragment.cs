using BulletSystem;
using DG.Tweening;
using UnityEngine;

namespace BossBehaviour.PhaseTemplates
{
    public abstract class BossPhaseMovementFragment : BossPhaseFragment
    {
        private static readonly int End = Animator.StringToHash("fragmentEndMove");
        public UpdateMethod updateMethod;
        protected BossController bossController;
        private bool endFlag;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            endFlag = false;
            animator.SetBool(End, false);
            bossController = BossBehaviourSystemProxy.instance.bossController;
            if (updateMethod is UpdateMethod.Classic)
            {
                bossController.bossMotion = BossMovementUpdate;
            }
            else
            {
                bossController.motionSequence.Kill();
                bossController.motionSequence = DOTween.Sequence();
                bossController.motionSequence.OnComplete(() => endFlag = true);
                BossTween(bossController.motionSequence);
                
            }

            CustomFragmentStart();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            bossController.bossMotion = null;
            CustomFragmentEnd();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (endFlag && updateMethod is UpdateMethod.Classic) return;
            if ((!FragmentEnd() || updateMethod is not UpdateMethod.Classic) &&
                (!endFlag || updateMethod is not UpdateMethod.DoTween)) return;
            animator.SetBool(End, true);
            endFlag = true;
        }

        protected virtual void CustomFragmentStart()
        {
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
    }
}