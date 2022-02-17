using System;
using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActiveCardImplementation
{
    public class TestCard : ActiveCard
    {
        private void Awake()
        {
            chargeTime = 2f;
            releaseTime = 1f;
        }

        
        public override void OnCharge(InputAction.CallbackContext context)
        {
            Debug.Log("Charge Started");
        }

        public override void OnTap(InputAction.CallbackContext context)
        {
            Debug.Log("Tapped");
        }
        
        public override void OnChargeComplete(InputAction.CallbackContext context)
        {
            Debug.Log("Charge Triggered.");
        }

        public override void OnChargeFailed(InputAction.CallbackContext context)
        {
            Debug.Log("Charge failed.");
        }

        public override void OnOverCharge(InputAction.CallbackContext context)
        {
            Debug.Log("Overcharged.");
        }
    }
}