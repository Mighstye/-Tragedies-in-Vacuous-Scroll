using Control;
using System.Collections.Generic;
using Utils;
using UnityEngine;

namespace BulletSystem
{
    public class ActiveBulletManager : Singleton<ActiveBulletManager>
    {
        public List<string> wipeableBulletTags = new List<string>();

        private void Start()
        {
            YoumuController.instance.onYoumuHit += Wipe;
            wipeableBulletTags.Add("EnemyBullet");
            //Spell.instance.onSpellUse += Wipe;
        }


        public void Wipe()
        {
            /* LEGACY
            var nbActiveBullets = this.transform.childCount;
            for(var i = 0; i < nbActiveBullets; i++)
            {
                var bullet = transform.GetChild(0).gameObject.GetComponent<Bullet>();
                if (bullet.gameObject.CompareTag("FriendlyBullet")) return;
                bullet.InvokeBulletDeath();
            } */
            foreach (var bullet in GetComponentsInChildren<Bullet>())
            {
                foreach (string wipeableBulletTag in wipeableBulletTags)
                {
                    if (bullet.gameObject.CompareTag(wipeableBulletTag))
                    {
                        bullet.InvokeBulletDeath();
                    }
                }
            }
        }
    }
}