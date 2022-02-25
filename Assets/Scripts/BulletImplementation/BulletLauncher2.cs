using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class BulletLauncher2 : BulletLauncher
    {
        public float shootFrequency;
        private float shootTimer = 0.0f;

        public int bulletSpeed;
        private float angle = 0;
        public float rotationangle;
        public float slideJump;

        private Vector3 initpos;
        [SerializeField]private GameObject upperleft;
        [SerializeField]private GameObject lowerright;

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
            {
                slideJump = -slideJump;
            }

            var transform1 = transform;
            var position = transform1.position;
            return new Vector3(position.x + slideJump, position.y, position.z);
        }

        private bool SquareShot()
        {
            shootTimer -= Time.deltaTime;

            Vector3 north = new Vector3(0,1,0) * bulletSpeed;
            Vector3 east = new Vector3(1,0,0) * bulletSpeed;
            Vector3 south = new Vector3(0,-1,0) * bulletSpeed;
            Vector3 west = new Vector3(-1,0,0) * bulletSpeed;

            if(shootTimer <= 0.0f)
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
                ((SimpleBullet)bulletN).Launch(position, north);
                ((SimpleBullet)bulletE).Launch(position, east);
                ((SimpleBullet)bulletS).Launch(position, south);
                ((SimpleBullet)bulletW).Launch(position, west);

                angle += rotationangle % (2*Mathf.PI);
                
                //position = SlideTransform();
                //transform.position = position;

                shootTimer = shootFrequency;
            }
            
            return false;
        }
    }
}