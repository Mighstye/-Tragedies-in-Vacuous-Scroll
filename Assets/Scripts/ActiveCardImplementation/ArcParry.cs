using CardSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using BulletSystem;
using Control;

namespace ActiveCardImplementation
{
    public class ArcParry : ActiveCard
    {

        public override void OnPreciseChargeCancelled(InputAction.CallbackContext context)
        {
            Debug.Log("OnPreciseChargeCancelled");
        }

        public override void OnPreciseChargePerformed(InputAction.CallbackContext context)
        {
            Debug.Log("OnPreciseChargePerformed");
        }

        public override void OnPreciseChargeStarted(InputAction.CallbackContext context)
        {
            Debug.Log("OnPreciseChargeStarted");
        }

        public override void OnSlowTapCancelled(InputAction.CallbackContext context)
        {
            Debug.Log("OnSlowTapCancelled");
        }

        public override void OnSlowTapPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("OnSlowTapPerformed");
        }

        public override void OnSlowTapStarted(InputAction.CallbackContext context)
        {
            Debug.Log("OnSlowTapStarted");
        }

        public override void OnTapCancelled(InputAction.CallbackContext context)
        {
            Debug.Log("OnTapCancelled");
        }

        public override void OnTapPerformed(InputAction.CallbackContext context)
        {
           
        }
    }
}