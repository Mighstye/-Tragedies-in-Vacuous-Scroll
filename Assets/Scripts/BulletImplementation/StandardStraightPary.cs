using System.Collections.Generic;
using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class StandardStraightPary: Bullet, ISimplePary
    {
        public Vector3 velocity { get; private set; }

        public override Vector3 getVelocity()
        {
            return velocity;
        }

        public void Launch(Vector3 position,Vector3 startVelocity)
        {
            ResetBullet();
            transform.position = new Vector3(position.x,position.y,0);
            velocity = startVelocity;
        }
        protected override void AddBehaviors()
        {
            behaviors.Add(StraightPropagate);
        }

        private bool StraightPropagate()
        {
            transform.position += velocity * Time.deltaTime;
            return false;
        }
        
        
    }
}