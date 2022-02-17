using System;
using System.Collections.Generic;
using Control;
using UnityEngine;
using BulletSystem;

namespace BulletSystem
{
    public abstract class BulletLauncher : MonoBehaviour
    {
        public BulletPool bulletPool;
        //Behavior
        /// <summary>
        /// A behaviour of a bullet should return <c>true</c> at the end of the behaviour and
        /// <c>false</c> otherwise. 
        /// </summary>
        protected delegate bool BulletLauncherBehavior();
        protected readonly List<BulletLauncherBehavior> behaviors = new List<BulletLauncherBehavior>();
        private int behaviorPointer = 0;

        protected abstract void AddBehaviors();

        private void Start()
        {
            AddBehaviors();
        }

        private void Update()
        {
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
    }
}
