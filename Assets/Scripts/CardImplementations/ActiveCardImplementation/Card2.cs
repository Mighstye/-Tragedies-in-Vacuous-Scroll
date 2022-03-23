using CardSystem;
using Control.ActiveCardControl.ControlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActiveCardImplementation
{
    public class Card2 : ActiveCard, ITappable
    {
        public string desc;
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

        public float tapTime { get; set; }

        public void OnTapPerformed(InputAction.CallbackContext context)
        {
            if (UseCard()) Use();
        }

        public void OnTapCancelled(InputAction.CallbackContext context)
        {
        }

        private void Use()
        {
            Debug.Log("Card2 used !");
        }
    }
}