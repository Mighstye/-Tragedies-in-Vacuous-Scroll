using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

public class Health : MonoBehaviour
{
    public static Health instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public int health;

    public int maxHealth;

    public float invincibilityTime;

    private bool invincible;

    private void Start()
    {
        health = maxHealth;
        invincible = false;
        Control.YoumuController.instance.onYoumuHit += () =>
        {
            if (!invincible)
            {
                LoseHealth();
                invincible = true;
                StartCoroutine(Invincibility());
            }

        };
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    public int get()
    {
        return health;
    }

    //retire de la vie, renvoie true si le joueur est encore en vie, false sinon
    public bool LoseHealth(int h)
    {
        if (health - h <= 0)
        {
            health = 0;
            return false;
        }

        health -= h;
        return true;
    }
    public bool LoseHealth()
    {
        return LoseHealth(1);
    }

    //ajoute de la vie si possible, renvoie true si la vie a été ajoutée, false sinon
    public bool GainHealth(int h)
    {
        if(health == maxHealth)
        {
            return false;
        }

        if (health + h > maxHealth)
        {
            health = maxHealth;
            return true;
        }

        health += h;
        return true;
    }
    public bool GainHealth()
    {
        return GainHealth(1);
    }
}
