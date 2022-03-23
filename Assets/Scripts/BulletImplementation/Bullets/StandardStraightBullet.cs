using BulletSystem;
using UnityEngine;

namespace BulletImplementation
{
    public class StandardStraightBullet : Bullet, ISimpleBullet
    {
        public Vector3 velocity { get; private set; }


        public void Launch(Vector3 position, Vector3 startVelocity)
        {
            ResetBullet();
            transform.position = new Vector3(position.x, position.y, 0);
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