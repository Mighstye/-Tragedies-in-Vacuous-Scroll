using System.Collections;
using Logic_System;
using UnityEngine;

namespace CardSystem
{
    public abstract class ActiveCard : Card
    {
        public int grazeCostSegment = 1; //DEFAULT VALUE

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
            coolDownCounter = commonMetadata.coolDown;
            while (coolDownCounter > 0)
            {
                coolDownCounter -= Time.deltaTime;
                coolDownProgress = coolDownCounter / commonMetadata.coolDown;
                yield return null;
            }

            coolDownProgress = 0;
            coolDown = false;
        }
    }
}