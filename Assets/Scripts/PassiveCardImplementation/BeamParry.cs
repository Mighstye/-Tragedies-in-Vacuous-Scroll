using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;
using BulletSystem;
using BulletImplementation;

namespace PassiveCardImplementation
{
    public class BeamParry : PassiveCard
    {
        public string desc;
        public BeamParry()
        {
            passiveDesc = desc;
        }
        private void Start()
        {
        }
        private void OnEnable()
        {
            SetLaserInfo(true);
        }

        private void OnDisable()
        {
            SetLaserInfo(false);
        }

        private static void SetLaserInfo(bool state)
        {
            //while(BulletInfoRegistry.instance is null){}
            var info = BulletInfoRegistry.instance.GetInfo(BulletTag.Laser);
            info.canBeParried = state;
            BulletInfoRegistry.instance.UpdateInfo(BulletTag.Laser,info);
        }
    } 
}
