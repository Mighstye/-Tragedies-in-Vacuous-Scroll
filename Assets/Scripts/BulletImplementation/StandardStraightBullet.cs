using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class StandardStraightBullet: Bullet
    {
        public Vector3 velocity { get; private set; }

        public void Launch(Vector3 position,Vector3 StartVelocity)
        {
            transform.position = new Vector3(position.x,position.y,0);
            velocity = StartVelocity;
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