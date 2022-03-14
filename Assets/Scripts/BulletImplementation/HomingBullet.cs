using System.Collections.Generic;
using UnityEngine;
using BulletSystem;
using Control;

namespace BulletImplementation
{
    public class HomingBullet : Bullet, ISimpleBullet
    {
        public Vector3 velocity { get; private set; }

        public float launchTime;
        private float launchTimer;

        public float idleTime;
        private float idleTimer;
        
        private Vector3 homingVector;

        public void Launch(Vector3 position, Vector3 startVelocity)
        {
            ResetBullet();
            launchTimer = launchTime;
            idleTimer = idleTime;
            transform.position = new Vector3(position.x, position.y, 0);
            velocity = startVelocity;
        }
        protected override void AddBehaviors()
        {
            behaviors.Add(StraightPropagate);
            behaviors.Add(Idle);
            behaviors.Add(AimForPlayer);
            behaviors.Add(GoForPlayer);
        }

        private bool StraightPropagate()
        {
            launchTimer -= Time.deltaTime;

            if(launchTimer <= 0.0f)
            {
                return true;
            }
            transform.position += velocity * Time.deltaTime;
            return false;
        }

        private bool Idle()
        {
            idleTimer -= Time.deltaTime;

            return idleTimer <= 0.0f;
        }

        private bool AimForPlayer()
        {
            var playerPos = YoumuController.instance.transform.position;
            var bulletPos = this.transform.position;
            var speed = velocity.magnitude*0.7f;

            homingVector = (playerPos - bulletPos).normalized * speed;

            return true;
        }

        private bool GoForPlayer()
        {
            transform.position += homingVector * Time.deltaTime;
            return false;
        }
        
    }
}