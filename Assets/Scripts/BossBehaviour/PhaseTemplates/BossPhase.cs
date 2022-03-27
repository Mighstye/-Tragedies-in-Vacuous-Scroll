using System;
using BulletSystem;
using DG.Tweening;
using Logic_System;
using UnityEngine;

namespace BossBehaviour.PhaseTemplates
{
    #region TypeEnums
    public enum PhaseEndType
         {
             DealDamage,
             Endure
         }
 
         public enum PhaseType
         {
             SpellPhase,
             NonSpellPhase
         }
         #endregion
    public abstract class BossPhase : StateMachineBehaviour
    {
        

        private static readonly int PhaseEnd = Animator.StringToHash("phaseEnd");
        [SerializeField] private int phaseFsmIndexNumber;
        [SerializeField] private string phaseName;
        [SerializeField] public PhaseEndType phaseEndType;
        [SerializeField] public PhaseType phaseType;
        [SerializeField] private float phaseDuration;
        [SerializeField] private int phaseHp;
        private BattleOutcome battleOutcome;
        private BossController bossController;
        private Animator phaseBehaviors;
        private Action phaseEnd;
        private PhaseIndex phaseIndex;
        private PhaseTimer phaseTimer;
        

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            phaseIndex = animator.GetComponent<PhaseIndex>();
            phaseBehaviors = phaseIndex.phaseStateMachines[phaseFsmIndexNumber];
            phaseBehaviors.gameObject.SetActive(true);
            phaseEnd = () => { SetPhaseEndVar(animator, true); };
            SetPhaseEndVar(animator, false);
            InitializeTimer(animator);
            InitializeBossController(animator);
            OnPhaseStartCustom(animator, stateInfo, layerIndex);
            battleOutcome = LogicSystemAPI.instance.battleOutcome;
            battleOutcome.RecordNewPhase(phaseType is PhaseType.NonSpellPhase ? null : phaseName);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SetBossIdle();
            battleOutcome.RegisterCurrentPhase();
            ActiveBulletManager.instance.Wipe();
            phaseBehaviors.gameObject.SetActive(false);
            phaseTimer.onPhaseTimeoutReached -= phaseEnd;
            if (bossController is null) return;
            bossController.onHpDepleted -= phaseEnd;
            OnPhaseEndCustom(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        protected virtual void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        protected virtual void OnPhaseEndCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        private void SetBossIdle()
        {
            bossController.bossMotion = null;
            bossController.motionSequence.Kill();
            bossController.animationLib.AnimationMove(0);
            bossController.SetUpHp(0);
        }

        private static void SetPhaseEndVar(Animator animator, bool state)
        {
            animator.SetBool(PhaseEnd, state);
        }

        private void InitializeTimer(Animator animator)
        {
            phaseTimer = PhaseTimer.instance;
            phaseTimer.SetUpTimer(phaseDuration, phaseName);
            phaseTimer.onPhaseTimeoutReached += phaseEnd;
        }

        private void InitializeBossController(Animator animator)
        {
            if (phaseEndType is not PhaseEndType.DealDamage) phaseHp = 0;
            bossController = BossBehaviourSystemProxy.instance.bossController;
            bossController.SetUpHp(phaseHp);
            bossController.onHpDepleted += phaseEnd;
        }

        protected string GetPhaseName()
        {
            return phaseName;
        }
    }
}