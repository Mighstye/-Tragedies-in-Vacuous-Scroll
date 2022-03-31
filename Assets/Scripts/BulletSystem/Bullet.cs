using System;
using System.Collections.Generic;
using System.Linq;
using Control;
using DG.Tweening;
using UnityEngine;

namespace BulletSystem
{
    public enum UpdateMethod
    {
        Classic,
        DoTween
    }

    public abstract class Bullet : MonoBehaviour, IBullet
    {
        //Update workflow
        public UpdateMethod updateMethod;

        //Tags
        public List<BulletTag> bulletTags;

        /// <summary>
        ///     List of behaviors that will be executed in FIFO order during the bullet's life.
        /// </summary>
        protected readonly List<BulletBehavior> behaviors = new();

        protected Sequence animationSequence;
        private int behaviorPointer;

        public Action onBulletParry;

        //Events
        /// <summary>
        ///     Invoked when the bullet is considered dead by <c>IsNaturallyDead</c>
        /// </summary>
        public Action onBulletDeathNatural { get; set; }

        /// <summary>
        ///     Invoked when other objects want to trigger the death of this bullet.
        ///     Will also trigger <c>onBulletDeathNatural </c>.
        /// </summary>
        public Action onBulletDeathManual { get; set; }

        //Graze
        public bool grazeable { get; set; }

        private void Awake()
        {
            AddBehaviors();
            ResetBullet();
        }


        private void Update()
        {
            if (IsNaturallyDead()) onBulletDeathNatural?.Invoke();
            CustomUpdate();
            if (updateMethod is UpdateMethod.DoTween) return;
            if (behaviors[behaviorPointer] != null && behaviors[behaviorPointer].Invoke()) behaviorPointer++;
        }

        protected virtual void AddBehaviors()
        {
            var methodInfos = this.GetType().GetMethods();
            foreach (var methodInfo in methodInfos) {
                var methodAttributes = methodInfo.GetCustomAttributes(true);
                foreach (Attribute attr in methodAttributes) {
                    if (attr is not BulletBehaviorFunc) continue;
                    var del = (BulletBehavior)Delegate.CreateDelegate(typeof(BulletBehavior),this, methodInfo);
                    behaviors.Add(del);
                }
            }
        }

        /// <summary>
        ///     Child class may overwrite this to provide extra update actions after bullet behaviour update.
        /// </summary>
        protected virtual void CustomUpdate()
        {
        }

        /// <summary>
        ///     Behavior that determines if a bullet is naturally dead.
        ///     By default, determines if the bullet is out of field.
        /// </summary>
        /// <returns><c>true</c> if out of field,<c>false</c> otherwise</returns>
        protected virtual bool IsNaturallyDead()
        {
            var pos = transform.position;
            return pos.x < FieldBoundaries.instance.left - 1 ||
                   pos.x > FieldBoundaries.instance.right + 1 ||
                   pos.y < FieldBoundaries.instance.down - 1 ||
                   pos.y > FieldBoundaries.instance.up + 1;
        }

        /// <summary>
        ///     Assign this bullet as a child of another transform (game object)
        /// </summary>
        /// <param name="parent">Transform of the gameObject the bullet should be child of</param>
        public void AssignToParent(Transform parent)
        {
            transform.parent = parent;
        }

        /// <summary>
        ///     Triggers the death of the bullet.
        /// </summary>
        public void InvokeBulletDeath()
        {
            if (!gameObject.activeInHierarchy) return;
            onBulletDeathManual?.Invoke();
            onBulletDeathNatural?.Invoke();
        }

        public void InvokeBulletParry()
        {
            onBulletParry?.Invoke();
        }

        public void ResetBullet()
        {
            if (updateMethod is UpdateMethod.DoTween) animationSequence.Play();
            grazeable = true;
            behaviorPointer = 0;
        }

        //Behavior
        /// <summary>
        ///     A behaviour of a bullet should return <c>true</c> at the end of the behaviour and
        ///     <c>false</c> otherwise.
        /// </summary>
        protected delegate bool BulletBehavior();
    }
}