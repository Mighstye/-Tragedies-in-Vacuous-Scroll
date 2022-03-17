using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Logic_System
{
    public class LogicSystemAPI : MonoBehaviour
    {
        public static LogicSystemAPI instance { get; private set; }
        
        public Health health;

        public Spell spell;

        public Graze graze;

        public BattleOutcome battleOutcome;

        public RewardSystem rewardSystem;
        private void Awake()
        {
            instance = this;
        }
    }
}
