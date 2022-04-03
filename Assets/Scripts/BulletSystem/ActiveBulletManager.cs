using Control;
using System.Collections.Generic;
using Utils;
using UnityEngine;
using System.Linq;

namespace BulletSystem
{
    public class ActiveBulletManager : Singleton<ActiveBulletManager>
    {
        private void Start()
        {
            YoumuController.instance.onYoumuHit += Wipe;
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
                if (bullet.gameObject.CompareTag("EnemyBullet") && bullet.bulletTags.Any(bulletTag => BulletInfoRegistry.instance.GetInfo(bulletTag).canBeWipedByPostHit))
                {
                    bullet.InvokeBulletDeath();
                }
            }
        }
    }
}