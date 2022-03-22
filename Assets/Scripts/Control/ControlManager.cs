using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class ControlManager : Singleton<ControlManager>
{
    private PlayerInput playerInput;
    
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void SwitchToDialogue()
    {
        playerInput.SwitchCurrentActionMap("Dialogue");
    }

    public void SwitchToPlayer()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
}
