using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utils;

namespace Logic_System
{
    public class LogicSystemAPI : Singleton<LogicSystemAPI>
    {

        public Health health;

        public Spell spell;

        public Graze graze;

        public BattleOutcome battleOutcome;

        public RewardSystem rewardSystem;
    }
}
