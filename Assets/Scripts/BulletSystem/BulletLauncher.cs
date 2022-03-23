using System.Collections.Generic;
using UnityEngine;

namespace BulletSystem
{
    public abstract class BulletLauncher : MonoBehaviour
    {
        public BulletPool bulletPool;
        protected readonly List<BulletLauncherBehavior> behaviors = new();
        private int behaviorPointer;

        private void Start()
        {
            AddBehaviors();
        }

        private void Update()
        {
            if (behaviors[behaviorPointer] != null && behaviors[behaviorPointer].Invoke()) behaviorPointer++;
            CustomUpdate();
        }

        protected abstract void AddBehaviors();

        /// <summary>
        ///     Child class may overwrite this to provide extra update actions after bullet behaviour update.
        /// </summary>
        protected virtual void CustomUpdate()
        {
        }

        //Behavior
        /// <summary>
        ///     A behaviour of a bullet should return <c>true</c> at the end of the behaviour and
        ///     <c>false</c> otherwise.
        /// </summary>
        protected delegate bool BulletLauncherBehavior();
    }
}