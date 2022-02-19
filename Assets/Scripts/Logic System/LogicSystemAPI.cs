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

    public Health Health { get; private set; } = new Health();
    [SerializeField] private int startingHealth = 2;
    public int maxHealth;
    public float invincibilityTime;

    public Spell Spell { get; private set; } = new Spell();
    public int spellDuration;
    [SerializeField] private int defaultSpellAmount = 3;
    public int maxSpell;

    public Graze Graze { get; private set; } = new Graze();
    public int defaultGrazeGain;
    public int grazeSegmentsNb;
    public int maxGraze;

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
}
