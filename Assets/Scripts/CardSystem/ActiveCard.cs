using UnityEngine;
using UnityEngine.InputSystem;
using Control.ActiveCardControl.ControlTypes;
using Logic_System;

namespace CardSystem
{
    public abstract class ActiveCard: Card
    {
        public int grazeCostSegment = 1; //DEFAULT VALUE

        protected bool useCard()
        {
            return LogicSystemAPI.instance.graze.UseGraze(grazeCostSegment);
        }
    }
}