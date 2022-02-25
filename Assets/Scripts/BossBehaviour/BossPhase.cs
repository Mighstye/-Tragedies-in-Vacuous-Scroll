using System;
using System.Collections;
using System.Collections.Generic;
using BossBehaviour;
using BulletSystem;
using UnityEngine;

public abstract class BossPhase : StateMachineBehaviour
{
    public enum PhaseType
    {
        DealDamage,
        Endure
    }

    [SerializeField] private int phaseFragmentsFSMIndexNumber=0;
    private PhaseFragmentIndex phaseFragmentIndex;
    private Animator phaseBehaviors;
    [SerializeField] private string phaseName;
    [SerializeField] private PhaseTimer phaseTimer;
    [SerializeField] private BossController bossController;
    [SerializeField] public PhaseType phaseType;
    [SerializeField] private float phaseDuration;
    [SerializeField] private int phaseHp;
    private static readonly int PhaseEnd = Animator.StringToHash("phaseEnd");
    private Action phaseEnd;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phaseFragmentIndex = animator.GetComponent<PhaseFragmentIndex>();
        phaseBehaviors = phaseFragmentIndex.phaseStateMachines[phaseFragmentsFSMIndexNumber];
        phaseBehaviors.enabled = true;
        phaseEnd = () => { SetPhaseEndVar(animator, true);};
        SetPhaseEndVar(animator,false);
        InitializeTimer(animator);
        InitializeBossController(animator);
        phaseBehaviors.enabled=true;
        OnPhaseStartCustom(animator, stateInfo,layerIndex);
        
    }

    protected virtual void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ActiveBulletManager.instance.Wipe();
        phaseBehaviors.enabled = false;
        phaseTimer.onPhaseTimeoutReached -= phaseEnd;
        if (bossController is null) return;
        bossController.onHpDepleted -= phaseEnd;

    }

    private static void SetPhaseEndVar(Animator animator, bool state)
    {
        animator.SetBool(PhaseEnd,state);
    }

    private void InitializeTimer(Animator animator)
    {
        phaseTimer = PhaseTimer.instance;
        phaseTimer.SetUpTimer(phaseDuration,phaseName);
        phaseTimer.onPhaseTimeoutReached += phaseEnd;
    }

    private void InitializeBossController(Animator animator)
    {
        
        if (phaseType is not PhaseType.DealDamage) return;
        bossController = BossController.instance;
        bossController.SetUpHp(phaseHp);
        bossController.onHpDepleted += phaseEnd;
    }
    
    
}
