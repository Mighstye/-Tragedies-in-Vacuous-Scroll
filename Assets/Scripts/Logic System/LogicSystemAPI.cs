using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic_System
{
    public class LogicSystemAPI : MonoBehaviour
    {
        public static LogicSystemAPI instance { get; private set; }
        
        public Health Health;

        public Spell Spell;

        public Graze Graze;

        private void Awake()
        {
            instance = this;
        }
    }
}
