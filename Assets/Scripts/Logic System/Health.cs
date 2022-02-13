using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    public static Health instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public int currentHealth { get; private set; }

    [SerializeField] private int startingHealth = 2;

    public int maxHealth;

    public float invincibilityTime;

    public bool invincible { get; private set; }
    public Action onNeedPlayerRefresh { get; set; }

    private float invincibleTimer = 0;

    private void Start()
    {
        currentHealth = startingHealth;
        invincible = false;
        Control.YoumuController.instance.onYoumuHit += () =>
        {
            if (invincible) return;
            LoseHealth();
            StartInvincible();
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
        Spell.instance.ReenableSpell();
    }
    
    
    private void GainHealth(int h = 1)
    {
        currentHealth = Mathf.Clamp(currentHealth + h, -1, maxHealth);
    }
    private bool LoseHealth(int h = 1)
    {
        GainHealth(-h);
        if (currentHealth < 0) return true;
        Spell.instance.SpellResetOnLifeLost();
        onNeedPlayerRefresh?.Invoke();
        return false;
    }
    public void StartInvincible(float time=0)
    {
        if (time == 0) time = invincibilityTime;
        invincible = true;
        invincibleTimer = time;
    }
}
