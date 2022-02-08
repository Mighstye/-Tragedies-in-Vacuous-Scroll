using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class BulletLauncher1 : BulletLauncher
    {
        public GameObject player;

        public int bulletNb;
        public int bulletSpeed;

        public float idleTime;
        public float launchTime;

        public float shootFrequency;
        private float shootTimer=0.0f;

        protected override void AddBehaviors()
        {
            behaviors.Add(SprayShoot);
        }

        private bool SprayShoot()
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0.0f)
            {
                Vector3 launchVector;

                for (int i = 0; i < bulletNb; i++)
                {
                    launchVector = new Vector3(-Mathf.Cos(0.7f + i * (1.7f / (bulletNb - 1))), -Mathf.Sin(0.7f + i * (1.7f / (bulletNb - 1))), 0);
                    launchVector *= bulletSpeed;

                    var bullet = bulletPool.pool.Get();
                    bullet.transform.SetParent(this.transform);
                    ((HomingBullet)bullet).player = player;
                    ((HomingBullet)bullet).idleTime = idleTime;
                    ((HomingBullet)bullet).launchTime = launchTime;
                    ((SimpleBullet)bullet).Launch(transform.position, launchVector);
                }

                shootTimer = shootFrequency;
            }

            return false;
        }


    }
}