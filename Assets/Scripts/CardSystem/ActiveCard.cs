using UnityEngine;
using UnityEngine.InputSystem;
using Control.ActiveCardControl.ControlTypes;

namespace CardSystem
{
    public abstract class ActiveCard: Card, ISlowTappable, IPreciseChargeable, ITappable
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
        public abstract void OnSlowTapStarted(InputAction.CallbackContext context);
        public abstract void OnSlowTapPerformed(InputAction.CallbackContext context);
        public abstract void OnSlowTapCancelled(InputAction.CallbackContext context);
        public abstract void OnPreciseChargeStarted(InputAction.CallbackContext context);
        public abstract void OnPreciseChargePerformed(InputAction.CallbackContext context);
        public abstract void OnPreciseChargeCancelled(InputAction.CallbackContext context);
        public abstract void OnTapPerformed(InputAction.CallbackContext context);
        public abstract void OnTapCancelled(InputAction.CallbackContext context);
    }
}