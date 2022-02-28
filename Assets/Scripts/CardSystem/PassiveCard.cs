using UnityEngine;
using Control;
using Control.YoumuController;
using LogicSystem;
using LogicSystem.Health;

namespace CardSystem
{
    public abstract class PassiveCard: Card
    {
        public static YoumuController instance{get; set;}
        protected Health healthBar;
        protected int frames;
    }
}