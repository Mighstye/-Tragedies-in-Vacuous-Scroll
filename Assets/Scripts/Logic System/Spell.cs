using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spell : MonoBehaviour
{
    public static Spell instance { get; private set; }

    public Action onSpellUse;
    private void Awake()
    {
        instance = this;
    }

    private int spell;

    public int maxSpell;

    private void Start()
    {
        spell = maxSpell;
    }

    public int get()
    {
        return spell;
    }

    //consomme le spell si possible, renvoie true si le spell a �t� consomm�, false sinon
    public bool UseSpell(int s)
    {
        if (spell - s < 0)
        {
            return false;
        }

        spell -= s;
        return true;
    }
    public bool UseSpell()
    {
        return UseSpell(1);
    }

    //ajoute du spell si possible, renvoie true si le spell a �t� ajout�e, false sinon
    public bool AddSpell(int s)
    {
        if(spell == maxSpell)
        {
            return false;
        }

        if (spell + s > maxSpell)
        {
            spell = maxSpell;
            return true;
        }

        spell += s;
        return true;
    }
    public bool AddSpell()
    {
        return AddSpell(1);
    }

    public void OnSpell(InputAction.CallbackContext context)
    {
        if (context.phase is not InputActionPhase.Performed) return;
        if (spell <= 0) return;
        UseSpell();
        onSpellUse?.Invoke();
        Health.instance.StartInvincible();
    }
}
