using System;
using UnityEngine;

namespace BulletSystem
{
    public class ActiveBulletManager : MonoBehaviour
    {
        public static ActiveBulletManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }
    }
}