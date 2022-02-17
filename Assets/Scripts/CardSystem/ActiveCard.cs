using UnityEngine;
using UnityEngine.InputSystem;

namespace CardSystem
{
    public abstract class ActiveCard: Card
    {
        /// <summary>
        /// chargeTime defines the minimum time the player should press to charge the card.
        /// if released before chargeTime, OnChargeFailed will be called. Note that there is
        /// 0.2 sec occupied by Tap interaction detection.
        /// </summary>
        [SerializeField] public float chargeTime { get; protected set; }
        /// <summary>
        /// if releaseTime is positive , then the player must release the button
        /// before the timer reaches releaseTime, otherwise it is considered as OverCharge.
        /// if release Time is negative or 0, then player can release the button
        /// anytime to trigger OnChargeComplete.
        /// The release check starts after chargeTime.
        /// </summary>
        [SerializeField] public float releaseTime { get;protected set; }
        private void OnSelect()
      {
              
      }

        /// <summary>
        /// Called when player Taps a button (press and release within 0.2 sec)
        /// </summary>
        /// <param name="context"></param>
      public virtual void OnTap(InputAction.CallbackContext context)
      {
          
      }

        /// <summary>
        /// Called when player successfully charges (press for at least chargeTime,
        /// and release before releaseTime, if applicable).
        /// </summary>
        /// <param name="context"></param>
      public virtual void OnChargeComplete(InputAction.CallbackContext context)
      {
          
      }

        /// <summary>
        /// Called when player holds the button more than 0.2 sec.
        /// </summary>
        /// <param name="context"></param>
      public virtual void OnCharge(InputAction.CallbackContext context)
      {
          
      }

        /// <summary>
        /// Called when player releases the button before chargeTime.
        /// </summary>
        /// <param name="context"></param>
      public virtual void OnChargeFailed(InputAction.CallbackContext context)
      {
          
      }

        /// <summary>
        /// Called when player releases the button after releaseTime.
        /// </summary>
        /// <param name="context"></param>
      public virtual void OnOverCharge(InputAction.CallbackContext context)
      {
          
      }
    }
    
    
}