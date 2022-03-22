using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class LaserLauncher1 : BulletLauncher
    {
        public float shootFrequency;
        private float shootTimer = 0.0f;

        public float homingTime;
        public float transitionTime;
        public float laserTime;
        public float laserWidth;

        protected override void AddBehaviors()
        {
            behaviors.Add(Shoot);
        }

        private bool Shoot()
        {
            shootTimer -= Time.deltaTime;

            if (!(shootTimer <= 0.0f)) return false;

            var laser = bulletPool.pool.Get();
            ((HomingLaser)laser).homingTime = homingTime;
            ((HomingLaser)laser).launcher = this;
            ((HomingLaser)laser).laserTime = laserTime;
            ((HomingLaser)laser).transitionTime = transitionTime;
            ((HomingLaser)laser).laserWidth = laserWidth;
            ((HomingLaser)laser).Launch();

            shootTimer = shootFrequency;

            return false;
        }


    }
}