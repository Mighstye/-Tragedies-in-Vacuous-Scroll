using BulletSystem;
using UnityEngine;

namespace BulletImplementation
{
    public class BulletLauncher2 : BulletLauncher
    {
        public float shootFrequency;

        public int bulletSpeed;
        public float rotationangle;
        public float slideJump;
        [SerializeField] private GameObject upperleft;
        [SerializeField] private GameObject lowerright;
        private float angle;

        private Vector3 initpos;
        private float shootTimer;

        protected override void AddBehaviors()
        {
            behaviors.Add(SquareShot);
        }

        private Vector3 RotateVector(Vector3 vector3)
        {
            return new Vector3(vector3.x * Mathf.Cos(angle) - vector3.y * Mathf.Sin(angle),
                vector3.x * Mathf.Sin(angle) + vector3.y * Mathf.Cos(angle), vector3.z);
        }

        private Vector3 SlideTransform()
        {
            if (transform.position.x + slideJump > lowerright.transform.position.x ||
                transform.position.x + slideJump < upperleft.transform.position.x)
                slideJump = -slideJump;

            var transform1 = transform;
            var position = transform1.position;
            return new Vector3(position.x + slideJump, position.y, position.z);
        }

        private bool SquareShot()
        {
            shootTimer -= Time.deltaTime;

            var north = new Vector3(0, 1, 0) * bulletSpeed;
            var east = new Vector3(1, 0, 0) * bulletSpeed;
            var south = new Vector3(0, -1, 0) * bulletSpeed;
            var west = new Vector3(-1, 0, 0) * bulletSpeed;

            if (shootTimer <= 0.0f)
            {
                var bulletN = bulletPool.pool.Get();
                var bulletE = bulletPool.pool.Get();
                var bulletS = bulletPool.pool.Get();
                var bulletW = bulletPool.pool.Get();

                north = RotateVector(north);
                east = RotateVector(east);
                south = RotateVector(south);
                west = RotateVector(west);

                var position = transform.position;
                ((ISimpleBullet)bulletN).Launch(position, north);
                ((ISimpleBullet)bulletE).Launch(position, east);
                ((ISimpleBullet)bulletS).Launch(position, south);
                ((ISimpleBullet)bulletW).Launch(position, west);

                angle += rotationangle % (2 * Mathf.PI);

                //position = SlideTransform();
                //transform.position = position;

                shootTimer = shootFrequency;
            }

            return false;
        }
    }
}