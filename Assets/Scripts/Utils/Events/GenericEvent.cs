using System.Collections.Generic;
using UnityEngine;

namespace Utils.Events
{
    public class GenericEvent<T> : ScriptableObject
    {
        private readonly List<GenericSubscriber<T>> subscribers = 
            new List<GenericSubscriber<T>>();
        
        public void Invoke(T context)
        {
            foreach (var t in subscribers)
            {
                t.OnEventFired(context);
            }
        }
        
        public static GenericEvent<T> operator+(GenericEvent<T> evt, GenericSubscriber<T> sub)
        {
            evt.subscribers.Add(sub);
            return evt;
        }

        public static GenericEvent<T> operator-(GenericEvent<T> evt, GenericSubscriber<T> sub)
        {
            evt.subscribers.Remove(sub);
            return evt;
        }
    }
}