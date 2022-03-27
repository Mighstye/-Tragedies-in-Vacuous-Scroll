using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utils.Events
{
    public class GenericSubscriber<T> : MonoBehaviour
    {
        public GenericEvent<T> gameEvent;
        public UnityEvent<T> unityEvent;

        public void OnEventFired(T context)
        {
            unityEvent?.Invoke(context);
        }

        private void OnEnable()
        {
            gameEvent += this;
        }

        private void OnDisable()
        {
            gameEvent -= this;
        }
    }
}