using UnityEngine.InputSystem;

namespace Control.ActiveCardControl.ControlTypes
{
    /// <summary>
    ///     Precise Charge requires player to press a button for at least <c>pressDuration</c> and
    ///     release within <c>releaseDuration</c> after <c>pressDuration</c> is reached.
    /// </summary>
    public interface IPreciseChargeable : IControlType
    {
        /// <summary>
        ///     Duration in seconds the player should press the button for.
        /// </summary>
        float pressDuration { get; set; }

        /// <summary>
        ///     Duration in seconds within which the player should release the button after pressDuration.
        /// </summary>
        float releaseDuration { get; set; }

        /// <summary>
        ///     Called when button press surpasses press point (0.8)
        /// </summary>
        /// <param name="context"></param>
        void OnPreciseChargeStarted(InputAction.CallbackContext context);

        /// <summary>
        ///     Called when button is released correctly.
        /// </summary>
        /// <param name="context"></param>
        void OnPreciseChargePerformed(InputAction.CallbackContext context);

        /// <summary>
        ///     Called when button is released too early or too late.
        /// </summary>
        /// <param name="context"></param>
        void OnPreciseChargeCancelled(InputAction.CallbackContext context);
    }
}