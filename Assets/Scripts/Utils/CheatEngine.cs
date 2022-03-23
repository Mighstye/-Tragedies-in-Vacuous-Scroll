using CardSystem;
using Logic_System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class CheatEngine : Singleton<CheatEngine>
{
    [SerializeField] private ActiveCard sample;
    private Health healthRef;


    private void Start()
    {
        healthRef = LogicSystemAPI.instance.health;
    }

    public void OnCheat1(InputAction.CallbackContext context)
    {
        if (context.phase is not InputActionPhase.Performed) return;
        LogicSystemAPI.instance.graze.AddGraze(50);
    }

    //Press W to activate
    public void OnCheat2(InputAction.CallbackContext context)
    {
        if (context.phase is not InputActionPhase.Performed) return;
        healthRef.GainHealth();
    }
}