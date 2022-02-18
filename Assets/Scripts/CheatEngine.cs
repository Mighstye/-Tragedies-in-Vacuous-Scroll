using System;
using System.Collections;
using System.Collections.Generic;
using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatEngine : MonoBehaviour
{
    public static CheatEngine instance { get; private set; }
    [SerializeField] private ActiveCard sample;

    private void Awake()
    {
        instance = this;
    }

    public void OnCheat1(InputAction.CallbackContext context)
    {
        if (context.phase is not InputActionPhase.Performed) return;
        var cheatCard = Instantiate(sample, this.gameObject.transform);
        CardSystemManager.instance.AddActiveCard(cheatCard);
    }

    //Press W to activate
    public void OnCheat2(InputAction.CallbackContext context)
    {
        if (context.phase is not InputActionPhase.Performed) return;
        LogicSystemAPI.instance.GainHealth();
    }
}
