using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance { get; private set; }
    private PlayerInput playerInput;

    private void Awake()
    {
        instance = this;
    }

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
