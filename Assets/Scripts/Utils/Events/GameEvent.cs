using System.Collections.Generic;
using UnityEngine;

namespace Utils.Events
{
    [CreateAssetMenu(fileName = "Event", menuName = "Events/Base Game Event (void->void)", order = 0)]
    public class GameEvent : ScriptableObject
    {
        [TextArea] [SerializeField] private string eventDescription;
        private readonly List<GameEventSubscriber> subscribers = 
            new List<GameEventSubscriber>();

        public void Invoke()
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