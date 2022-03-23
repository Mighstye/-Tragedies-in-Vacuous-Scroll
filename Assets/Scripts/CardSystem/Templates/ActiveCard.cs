using System.Collections;
using Logic_System;
using UnityEngine;

namespace CardSystem
{
    public abstract class ActiveCard : Card
    {
        public int grazeCostSegment = 1; //DEFAULT VALUE

        public float coolDownTime = 3; //DEFAULT VALUE

        public float coolDownProgress;

        private float coolDownCounter;

        public bool coolDown { get; private set; }


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