using CardSystem;
using Control.ActiveCardControl.ControlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActiveCardImplementation
{
    public class TestCard : ActiveCard, ISlowTappable,IPreciseChargeable, ITappable
    {
        public float tapTime { get; set; }
        public float slowTapTime { get; set; }
        public float pressDuration { get; set; }
        public float releaseDuration { get; set; }

        private void Start()
        {
            tapTime = 0.5f;
            slowTapTime = 2;
            pressDuration = 2;
            releaseDuration = 2;
        }

        public void OnSlowTapStarted(InputAction.CallbackContext context)
        {
            Debug.Log("Slow Tap Started");
        }

        public void OnSlowTapPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Slow Tap success");
        }

        public void OnSlowTapCancelled(InputAction.CallbackContext context)
        {
            Debug.Log("Slow Tap fail");
        }

       
        public void OnPreciseChargeStarted(InputAction.CallbackContext context)
        {
            Debug.Log("Precise Charge Started");
        }

        public void OnPreciseChargePerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Precise Charge Success");
        }

        public void OnPreciseChargeCancelled(InputAction.CallbackContext context)
        {
            Debug.Log("Precise Charge Failed");
        }

       
        public void OnTapPerformed(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnTapCancelled(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}