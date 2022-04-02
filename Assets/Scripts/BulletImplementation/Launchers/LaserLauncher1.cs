using BulletSystem;
using UnityEngine;

namespace BulletImplementation
{
    public class LaserLauncher1 : BulletLauncher
    {
        public LaserPool laserPool;

        public float timeBetweenShoots;

        public float homingTime;
        public float transitionTime;
        public float laserTime;
        public float laserWidth;
        private float shootTimer;
        private float transitionTimer;
        private bool isLaserDead;

        private HomingLaserCue laserCue;
        private HomingLaser laser;

        protected override void AddBehaviors()
        {
            behaviors.Add(LauchHoming);
            behaviors.Add(Homing);
            behaviors.Add(Transition);
            behaviors.Add(Shoot);
            behaviors.Add(WaitForLaserDeath);
            behaviors.Add(WaitBetweenShoots);
        }

        private bool LauchHoming()
        {
            laserCue = (HomingLaserCue)bulletPool.pool.Get();
            laserCue.homingTime = homingTime;
            laserCue.launcher = this;
            laserCue.laserWidth = laserWidth;
            laserCue.Launch();

            shootTimer = timeBetweenShoots;
            transitionTimer = transitionTime;
            isLaserDead = false;

            return true;
        }

        private bool Homing()
        {
            if (laserCue.isAimed) return true;
            return false;
        }

        public bool Transition()
        {
            transitionTimer -= Time.deltaTime;
            if (!(transitionTimer <= 0.0f)) return false;
            return true;
        }

        private bool Shoot()
        {
            laser = (HomingLaser)laserPool.pool.Get();
            laser.launcher = this;
            laser.laserTime = laserTime;
            laser.laserWidth = laserWidth;
            laser.startPos = laserCue.startPos;
            laser.endPos = laserCue.endPos;

            laser.laserDeath.AddListener(LaserDeath);

            laser.Launch();

            return true;
        }

        private bool WaitForLaserDeath()
        {
            if (isLaserDead) return true;
            return false;
        }

        private bool WaitBetweenShoots()
        {
            shootTimer -= Time.deltaTime;

            if (!(shootTimer <= 0.0f)) return false;

            behaviorPointer = -1;
            return true;
        }

        private void LaserDeath()
        {
            laser.laserDeath.RemoveListener(LaserDeath);
            laserCue.InvokeBulletDeath(); ;
            isLaserDead = true;
        }
    }
}