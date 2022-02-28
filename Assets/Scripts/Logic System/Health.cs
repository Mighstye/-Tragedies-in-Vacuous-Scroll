using System;
using System.Collections;
using System.Collections.Generic;
using Logic_System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic_System
{
    public class Health : MonoBehaviour
    {
        private LogicSystemAPI logic;
        public int currentHealth { get; private set; }

        [SerializeField] private int startingHealth = 2;

        public int maxHealth;

        public float invincibilityTime;

        public bool invincible { get; private set; }
        
        private float invincibleTimer = 0;

        public Action onNeedPlayerRefresh { get; set; }

        public Action onPlayerDeath { get; set; }

        private void Start()
        {
            logic = LogicSystemAPI.instance;
            currentHealth = startingHealth;
            invincible = false;
            Control.YoumuController.instance.onYoumuHit += () =>
            {
                if (invincible) return;
                if (LoseHealth())
                {
                    if (currentHealth == 0) onNeedPlayerRefresh?.Invoke();
                    onPlayerDeath?.Invoke();
                    return;
                }
                else
                {
                    StartInvincible();
                }
            };
        }

        private void Update()
        {
            if (invincible)
            {
                invincibleTimer -= Time.deltaTime;
            }

            if (!(invincibleTimer <= 0)) return;
            invincible = false;
           logic.Spell.ReenableSpell();
        }


        public void GainHealth(int h = 1)
        {
            currentHealth = Mathf.Clamp(currentHealth + h, -1, maxHealth);
        }
        private bool LoseHealth(int h = 1)
        {
            GainHealth(-h);
            if (currentHealth <= 0) return true;
            logic.Spell.SpellResetOnLifeLost();
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
