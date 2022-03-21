using System;
using System.Collections;
using System.Collections.Generic;
using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Logic_System;

public class CheatEngine : MonoBehaviour
{
    private Health healthRef;
    public static CheatEngine instance { get; private set; }
    [SerializeField] private ActiveCard sample;

    private void Awake()
    {
        instance ??= this;
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

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
