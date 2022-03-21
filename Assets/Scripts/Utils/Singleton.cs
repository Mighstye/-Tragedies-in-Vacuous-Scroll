using System;
using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour
    {
        public static T instance { get; private set; }
        private void Awake()
        {

        }
    }
}