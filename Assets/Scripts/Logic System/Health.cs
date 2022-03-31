using System;
using Control;
using UnityEngine;
using Utils.Events;

namespace Logic_System
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 2;

        public int maxHealth;

        public float invincibilityTime;

        private float invincibleTimer;
        private LogicSystemAPI logic;
        public int currentHealth { get; private set; }

        public bool invincible { get; private set; }

        public Action onNeedPlayerRefresh { get; set; }

        [SerializeField] private GameEvent onZeroLife;

        [SerializeField] private GameEvent onHitResolve;

        private void Start()
        {
            logic = LogicSystemAPI.instance;
            currentHealth = startingHealth;
            invincible = false;
            YoumuController.instance.onYoumuHit += () =>
            {
                if (invincible) return;
                if (LoseHealth())
                {
                    if (currentHealth == 0) onNeedPlayerRefresh?.Invoke();
                    onZeroLife.Invoke();
                    return;
                }

                StartInvincible();
            };
        }

        private void Update()
        {
            if (invincible) invincibleTimer -= Time.deltaTime;

            if (!(invincibleTimer <= 0)) return;
            invincible = false;
            logic.spell.ReenableSpell();
        }


        public void GainHealth(int h = 1)
        {
            currentHealth = Mathf.Clamp(currentHealth + h, -1, maxHealth);
        }

        private bool LoseHealth(int h = 1)
        {
            GainHealth(-h);
            onHitResolve.Invoke();
            if (currentHealth <= 0) return true;
            logic.spell.SpellResetOnLifeLost();
            onNeedPlayerRefresh?.Invoke();
            return false;
        }

        public void StartInvincible(float time = 0)
        {
            if (time == 0) time = invincibilityTime;
            invincible = true;
            invincibleTimer = time;
        }
    }
}