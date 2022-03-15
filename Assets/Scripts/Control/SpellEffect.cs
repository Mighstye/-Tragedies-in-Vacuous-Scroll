using System;
using System.Collections;
using System.Collections.Generic;
using BulletSystem;
using Logic_System;
using UnityEngine;
using UnityEngine.VFX;

public class SpellEffect : MonoBehaviour
{
    
    [SerializeField] private VisualEffect spellVFX;
    // Start is called before the first frame update

    private Spell spellRef;

    public delegate void SpellExecution(Collider2D col);

    private SpellExecution spellExecution;
    private void Start()
    {
        spellRef = LogicSystemAPI.instance.spell;

        spellRef.onSpellUse += () =>
        {
            gameObject.SetActive(true);
            StartCoroutine(StartEffect());
        };
        
        gameObject.SetActive(false);
        spellExecution = DefaultSpellExecution;
    }

    private IEnumerator StartEffect()
    {
        spellVFX.Play();
        yield return new WaitForSeconds(spellRef.spellDuration);
        spellVFX.Reinit();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        spellExecution?.Invoke(col);
    }

    private static void DefaultSpellExecution(Collider2D col)
    {
        if (!col.gameObject.CompareTag("EnemyBullet")) return;
        var bullet = col.gameObject.GetComponent<Bullet>();
        if (bullet!=null)
        {
            bullet.InvokeBulletDeath();
        }
    }

    public void RedefineSpellExecution(SpellExecution exec = null)
    {
        if (exec is null) spellExecution = DefaultSpellExecution;
        spellExecution = exec;
    }
}
