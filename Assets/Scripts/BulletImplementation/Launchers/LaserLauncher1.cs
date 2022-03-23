using BulletSystem;
using UnityEngine;

namespace BulletImplementation
{
    public class LaserLauncher1 : BulletLauncher
    {
        public float shootFrequency;

        public float homingTime;
        public float transitionTime;
        public float laserTime;
        public float laserWidth;
        private float shootTimer;

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