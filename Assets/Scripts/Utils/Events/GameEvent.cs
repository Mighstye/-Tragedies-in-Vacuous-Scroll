using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "Event", menuName = "Events/Base Game Event", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventSubscriber> subscribers = 
            new List<GameEventSubscriber>();

        public void FireEvent()
        {
            foreach (var t in subscribers)
            {
                t.OnEventFired();
            }
        }

        public static GameEvent operator+(GameEvent evt, GameEventSubscriber sub)
        {
            evt.subscribers.Add(sub);
            return evt;
        }

        public static GameEvent operator-(GameEvent evt, GameEventSubscriber sub)
        {
            evt.subscribers.Remove(sub);
            return evt;
        }
    }
}