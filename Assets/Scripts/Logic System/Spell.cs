using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic_System
{
    public class Spell
    {
        public int spellDuration;
        public int defaultSpellAmount;
        public int currentSpellAmount { get; private set; }

        public int maxSpell;
        private bool inSpellEffect;

        public void Start()
        {
            currentSpellAmount = defaultSpellAmount;
            inSpellEffect = false;
        }

        private void AddSpell(int s = 1)
        {
            currentSpellAmount = Mathf.Clamp(currentSpellAmount + s, 0, maxSpell);
            LogicSystemAPI.instance.onNeedSpellRefresh?.Invoke();
        } 
        private void UseSpell(int s = 1)
        {
            AddSpell(-s);
        }

        public void OnSpell(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            if (currentSpellAmount <= 0) return;
            TriggerSpell();
        }

        private void TriggerSpell()
        {
            if (inSpellEffect) return;
            UseSpell();
            inSpellEffect = true;
            LogicSystemAPI.instance.onSpellUse?.Invoke();
            LogicSystemAPI.instance.StartInvincible(spellDuration);
        }

        public void ReenableSpell()
        {
            inSpellEffect = false;
        }

        public void SpellResetOnLifeLost()
        {
            currentSpellAmount = defaultSpellAmount;
            LogicSystemAPI.instance.onNeedSpellRefresh?.Invoke();
        }
    }
}
