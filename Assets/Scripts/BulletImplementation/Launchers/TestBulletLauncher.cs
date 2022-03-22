using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class TestBulletLauncher : BulletLauncher
    {
        public Vector3 launchVector;

        public int x;
        private int frameCounter = 0;
        protected override void AddBehaviors()
        {
            behaviors.Add(ShootEveryXFrames);
        }

        private bool ShootEveryXFrames()
        {
            if(frameCounter == x)
            {
                var bullet = bulletPool.pool.Get();
                ((ISimpleBullet)bullet).Launch(transform.position, launchVector);

                frameCounter = 0;
            }
            else
            {
                frameCounter++; ;
            }

            return false;
        }


    }
}