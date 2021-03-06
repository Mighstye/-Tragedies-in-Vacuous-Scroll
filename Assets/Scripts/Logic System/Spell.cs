using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic_System
{
    public class Spell : MonoBehaviour
    {
        private LogicSystemAPI logic;
        public int spellDuration;
        [SerializeField] private int defaultSpellAmount = 3;
        public int currentSpellAmount { get; private set; }

        public int maxSpell;
        private bool inSpellEffect;

        public Action onSpellUse;
        public Action onNeedSpellRefresh { get; set; }

        private void Start()
        {
            logic=LogicSystemAPI.instance;
            currentSpellAmount = defaultSpellAmount;
            inSpellEffect = false;
        }

        private void AddSpell(int s = 1)
        {
            currentSpellAmount = Mathf.Clamp(currentSpellAmount + s, 0, maxSpell);
            onNeedSpellRefresh?.Invoke();
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
            onSpellUse?.Invoke();
            logic.health.StartInvincible(spellDuration);
        }

        public void ReenableSpell()
        {
            inSpellEffect = false;
        }

        public void SpellResetOnLifeLost()
        {
            currentSpellAmount = defaultSpellAmount;
            onNeedSpellRefresh?.Invoke();
        }
    }
}
