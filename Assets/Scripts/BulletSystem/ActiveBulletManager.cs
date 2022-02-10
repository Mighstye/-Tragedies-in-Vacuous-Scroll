using System;
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

        public void wipe()
        {
            int nbActiveBullets = this.transform.childCount;
            for(int i = 0; i < nbActiveBullets; i++)
            {
                var bullet = this.transform.GetChild(0).gameObject.GetComponent<Bullet>();
                bullet.InvokeBulletDeath();
            }
        }
    }
}