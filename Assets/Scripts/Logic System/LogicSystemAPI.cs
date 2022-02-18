using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Logic_System;

public class LogicSystemAPI : MonoBehaviour
{
    public static LogicSystemAPI instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private Health Health = new Health();
    [SerializeField] private int startingHealth = 2;
    public int maxHealth;
    public float invincibilityTime;
    public Action onNeedPlayerRefresh { get; set; }

    private Spell Spell = new Spell();
    public int spellDuration;
    [SerializeField] private int defaultSpellAmount = 3;
    public int maxSpell;
    public Action onSpellUse;
    public Action onNeedSpellRefresh { get; set; }

    private Graze Graze = new Graze();
    public int defaultGrazeGain;
    public int grazeSegmentsNb;
    public int maxGraze;
    public Action onNeedGrazeRefresh { get; set; }

    private void Start()
    {
        Health.startingHealth = startingHealth;
        Health.maxHealth = maxHealth;
        Health.invincibilityTime = invincibilityTime;
        Health.Start();

        Spell.spellDuration = spellDuration;
        Spell.defaultSpellAmount = defaultSpellAmount;
        Spell.maxSpell = maxSpell;
        Spell.Start();

        Graze.defaultGrazeGain = defaultGrazeGain;
        Graze.grazeSegmentsNb = grazeSegmentsNb;
        Graze.maxGraze = maxGraze;
    }

    private void Update()
    {
        Health.Update();
    }

    public int getCurrentHealth() {
        return Health.currentHealth;
    }
    public bool isInvincible() {
        return Health.invincible;
    }
    public void GainHealth(int h = 1)
    {
        Health.GainHealth(h);
    }
    public void StartInvincible(float time = 0)
    {
        Health.StartInvincible(time);
    }

    public int getCurrentSpellAmount() {
        return Spell.currentSpellAmount;    
    }
    public void OnSpell(InputAction.CallbackContext context)
    {
        Spell.OnSpell(context);
    }
    public void ReenableSpell()
    {
        Spell.ReenableSpell();
    }
    public void SpellResetOnLifeLost()
    {
        Spell.SpellResetOnLifeLost();
    }

    public int getGraze()
    {
        return Graze.get();
    }
    public bool AddGraze(int g)
    {
        return Graze.AddGraze(g);
    }
    public bool AddGraze()
    {
        return AddGraze(defaultGrazeGain);
    }
    public bool UseGraze(int nbSegments)
    {
        return Graze.UseGraze(nbSegments);
    }
    public bool UseGraze()
    {
        return UseGraze(1);
    }

}
