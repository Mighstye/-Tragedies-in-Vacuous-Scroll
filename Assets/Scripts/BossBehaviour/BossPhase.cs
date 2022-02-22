using System;
using System.Collections;
using System.Collections.Generic;
using BulletSystem;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    public enum PhaseType
    {
        DealDamage,
        Endure
    }

    
    [SerializeField] public PhaseType phaseType;
    [SerializeField] private float phaseDuration;
    [SerializeField] private float phaseHp;
    [SerializeField] private List<BulletLauncher> launchers;
    private float phaseTimer;
    private float currentPhaseHp;
    private void OnPhaseStart()
    {
        
    }

    private void OnPhaseEnd()
    {
        
    }

    private bool PhaseEndCondition()
    {
        return currentPhaseHp <= 0 && phaseType is PhaseType.DealDamage || phaseDuration <= 0;
    }

    private void Update()
    {
        phaseTimer -= Time.deltaTime;
    }
}
