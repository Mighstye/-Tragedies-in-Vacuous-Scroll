using UnityEngine;
using UnityEngine.InputSystem;
using Control.ActiveCardControl.ControlTypes;
using Logic_System;

namespace CardSystem
{
    public abstract class ActiveCard: Card
    {
        protected int grazeCostSegment = 1; //DEFAULT VALUE

        protected bool useCard()
        {
            if (!(LogicSystemAPI.instance.Graze.get() >= grazeCostSegment)) return false;
            else
            {
                LogicSystemAPI.instance.Graze.UseGraze(grazeCostSegment);
                return true;
            }
        }
    }
}