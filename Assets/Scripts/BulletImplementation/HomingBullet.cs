using System.Collections.Generic;
using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class HomingBullet : Bullet, SimpleBullet
    {
        public Vector3 velocity { get; private set; }

        public float launchTime;
        private float launchTimer;

        public float idleTime;
        private float idleTimer;

        public GameObject player;
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

            if (idleTimer <= 0.0f)
            {
                return true;
            }

            return false;
        }

        private bool AimForPlayer()
        {
            Vector3 playerPos = player.transform.position;
            Vector3 bulletPos = this.transform.position;
            float speed = velocity.magnitude;

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