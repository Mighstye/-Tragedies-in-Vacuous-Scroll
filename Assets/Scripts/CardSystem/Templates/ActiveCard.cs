using UnityEngine;
using UnityEngine.InputSystem;
using Control.ActiveCardControl.ControlTypes;
using Logic_System;
using System.Collections;

namespace CardSystem
{
    public abstract class ActiveCard: Card
    {
        public int grazeCostSegment = 1; //DEFAULT VALUE

        public float coolDownTime = 3; //DEFAULT VALUE

        private bool coolDown = false;

        private float coolDownCounter=0;

        public float coolDownProgress=0;
        
        

        protected bool UseCard()
        {
            if (coolDown) return false;
            if (!LogicSystemAPI.instance.graze.UseGraze(grazeCostSegment)) return false;
            coolDown = true;
            StartCoroutine(CoolDown());
            return true;
        }

        private IEnumerator CoolDown()
        {
            coolDownCounter = coolDownTime;
            while (coolDownCounter > 0)
            {
                coolDownCounter -= Time.deltaTime;
                coolDownProgress = coolDownCounter / coolDownTime;
                yield return null;
            }
            coolDownProgress = 0;
            coolDown = false;
        }
    }
}