using System;
using System.Collections.Generic;
using BossBehaviour;
using UnityEngine;
using BulletSystem;

namespace BulletImplementation
{
    public class StandardStraightParry: Bullet, ISimpleParry
    {
        private Vector3 direction { get; set; }
        public float speed=2;
        public int damage
        {
            get => 1;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                damage = value;
            }
        }
        

        public void Launch(Vector3 position,Vector3 startVelocity)
        {
            ResetBullet();
            transform.position = new Vector3(position.x,position.y,0);
            direction = startVelocity;
        }
        protected override void AddBehaviors()
        {
            behaviors.Add(StraightPropagate);
        }

        private bool StraightPropagate()
        {
            var transform1 = transform;
            var position = transform1.position;
            direction = (BossBehaviourSystemProxy.instance.bossController.transform.position - position).normalized;
            position += direction * Time.deltaTime * speed;
            transform1.position = position;
            return false;
        }


        
    }
}