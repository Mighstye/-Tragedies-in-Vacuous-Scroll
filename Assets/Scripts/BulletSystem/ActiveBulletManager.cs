using System;
using Control;
using UnityEngine;

namespace BulletSystem
{
    public class ActiveBulletManager : MonoBehaviour
    {
        public static ActiveBulletManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            YoumuController.instance.onYoumuHit += Wipe;
            Spell.instance.onSpellUse += Wipe;
        }


        private void Wipe()
        {
            var nbActiveBullets = this.transform.childCount;
            for(var i = 0; i < nbActiveBullets; i++)
            {
                var bullet = this.transform.GetChild(0).gameObject.GetComponent<Bullet>();
                bullet.InvokeBulletDeath();
            }
        }
    }
}