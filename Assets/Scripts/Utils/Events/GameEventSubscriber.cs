using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class GameEventSubscriber : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent unityEvent;

        public void OnEventFired()
        {
            unityEvent?.Invoke();
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