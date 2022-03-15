using System;
using System.Collections.Generic;
using Control;
using UnityEngine;

namespace BulletSystem
{
    public abstract class Bullet : MonoBehaviour, IBullet
    {
        //Tags
        public List<BulletTag> bulletTags;
        //Events
        /// <summary>
        /// Invoked when the bullet is considered dead by <c>IsNaturallyDead</c>
        /// </summary>
        public Action onBulletDeathNatural { get; set; }
        /// <summary>
        /// Invoked when other objects want to trigger the death of this bullet.
        /// Will also trigger <c>onBulletDeathNatural </c>.
        /// </summary>
        public Action onBulletDeathManual { get; set; }

        public Action onBulletParry;
        //Behavior
        /// <summary>
        /// A behaviour of a bullet should return <c>true</c> at the end of the behaviour and
        /// <c>false</c> otherwise. 
        /// </summary>
        protected delegate bool BulletBehavior();
        /// <summary>
        /// List of behaviors that will be executed in FIFO order during the bullet's life.
        /// </summary>
        protected readonly List<BulletBehavior> behaviors = new List<BulletBehavior>();
        private int behaviorPointer = 0;

        //Graze
        public bool grazeable { get; set; }
        private void Start()
        {
            ResetBullet();
            AddBehaviors();
        }

        protected abstract void AddBehaviors();

        //public abstract Vector3 getVelocity();

        private void Update()
        {
            if (IsNaturallyDead())
            {
                onBulletDeathNatural?.Invoke();
            }

            if (behaviors[behaviorPointer] != null && behaviors[behaviorPointer].Invoke())
            {
                behaviorPointer++;
            }
            CustomUpdate();
        }

        /// <summary>
        /// Child class may overwrite this to provide extra update actions after bullet behaviour update.
        /// </summary>
        protected virtual void CustomUpdate()
        {
            
        }

        /// <summary>
        /// Behavior that determines if a bullet is naturally dead. 
        /// By default, determines if the bullet is out of field.
        /// </summary>
        /// <returns><c>true</c> if out of field,<c>false</c> otherwise</returns>
        protected virtual bool IsNaturallyDead()
        {
            var pos = transform.position;
            return (pos.x < FieldBoundaries.instance.left - 1 ||
                    pos.x > FieldBoundaries.instance.right + 1 ||
                    pos.y < FieldBoundaries.instance.down - 1 ||
                    pos.y > FieldBoundaries.instance.up + 1);
        }

        /// <summary>
        /// Assign this bullet as a child of another transform (game object)
        /// </summary>
        /// <param name="parent">Transform of the gameObject the bullet should be child of</param>
        public void AssignToParent(Transform parent)
        {
            transform.parent = parent;
        }

        /// <summary>
        /// Triggers the death of the bullet.
        /// </summary>
        public void InvokeBulletDeath()
        {
            onBulletDeathManual?.Invoke();
            onBulletDeathNatural?.Invoke();
        }

        public void InvokeBulletParry()
        {
            onBulletParry?.Invoke();
        }

        public void ResetBullet()
        {
            grazeable = true;
            behaviorPointer = 0;
        }
        
    }
}
