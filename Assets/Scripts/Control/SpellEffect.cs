using System.Collections;
using BulletSystem;
using Logic_System;
using UnityEngine;
using UnityEngine.VFX;

public class SpellEffect : MonoBehaviour
{
    public delegate void SpellExecution(Collider2D col);

    [SerializeField] private VisualEffect spellVFX;

    private SpellExecution spellExecution;
    // Start is called before the first frame update

    private Spell spellRef;

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        spellExecution?.Invoke(col);
    }

    private IEnumerator StartEffect()
    {
        spellVFX.Play();
        yield return new WaitForSeconds(spellRef.spellDuration);
        spellVFX.Reinit();
        gameObject.SetActive(false);
    }

    private static void DefaultSpellExecution(Collider2D col)
    {
        if (!col.gameObject.CompareTag("EnemyBullet")) return;
        var bullet = col.gameObject.GetComponent<Bullet>();
        if (bullet != null) bullet.InvokeBulletDeath();
    }

    public void RedefineSpellExecution(SpellExecution exec = null)
    {
        exec ??= DefaultSpellExecution;
        spellExecution = exec;
    }
}