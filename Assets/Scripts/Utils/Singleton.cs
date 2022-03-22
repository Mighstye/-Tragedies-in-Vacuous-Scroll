using System;
using System.Data;
using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T: Component
    {
        public static T instance { get; private set; }
        protected bool destructionProtection = false;
        protected virtual void Awake()
        {
            if (instance is null)
            {
                instance = this as T;
                if(destructionProtection) DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}