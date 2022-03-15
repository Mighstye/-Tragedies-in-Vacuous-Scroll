using System;
using System.Collections;
using System.Collections.Generic;
using BossBehaviour;
using BulletSystem;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BossPhase : StateMachineBehaviour
{
    public enum PhaseType
    {
        DealDamage,
        Endure
    }

    [SerializeField] private int phaseFsmIndexNumber=0;
    private PhaseIndex phaseFragmentIndex;
    private GameObject phaseBehaviors;
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
        phaseFragmentIndex = animator.GetComponent<PhaseIndex>();
        phaseBehaviors = phaseFragmentIndex.phaseStateMachines[phaseFsmIndexNumber];
        phaseBehaviors.SetActive(true);
        phaseEnd = () => { SetPhaseEndVar(animator, true);};
        SetPhaseEndVar(animator,false);
        InitializeTimer(animator);
        InitializeBossController(animator);
        phaseBehaviors.SetActive(true);
        OnPhaseStartCustom(animator, stateInfo,layerIndex);
        
    }

    protected virtual void OnPhaseStartCustom(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ActiveBulletManager.instance.Wipe();
        phaseBehaviors.SetActive(false);
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
        bossController = BossBehaviourSystemProxy.instance.bossController;
        bossController.SetUpHp(phaseHp);
        bossController.onHpDepleted += phaseEnd;
    }
    
}
