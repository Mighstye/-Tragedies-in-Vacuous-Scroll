using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Control.ActiveCardControl.ControlTypes;
using System.Collections.Generic;
using System.Linq;
using BulletImplementation;
using BulletSystem;
using Utils;
using Control;
using System;

namespace ActiveCardImplementation
{
    public class Card2 : ActiveCard, ITappable
    {
        public float tapTime { get; set; }
        public float slowTapTime { get; set; }
        public float pressDuration { get; set; }
        public float releaseDuration { get; set; }
        public string desc;
        

        private void Start()
        {
            tapTime = 0.5f;
            slowTapTime = 2;
            pressDuration = 2;
            releaseDuration = 2;
        }

        public void OnTapPerformed(InputAction.CallbackContext context)
        {
            if (UseCard() == true) Use();
        }

        public void OnTapCancelled(InputAction.CallbackContext context)
        {
            return;
        }

        private void Use()
        {
            Debug.Log("Card2 used !");
        }
    }
}