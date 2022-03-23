using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected bool destructionProtection = false;
        public static T instance { get; private set; }

        protected virtual void Awake()
        {
            if (instance is null)
            {
                instance = this as T;
                if (destructionProtection) DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}