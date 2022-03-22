using UnityEngine.InputSystem;

namespace Control.ActiveCardControl.ControlTypes
{
    public interface ISlowTappable: IControlType
    {
        float slowTapTime { get; set; }
        void OnSlowTapStarted(InputAction.CallbackContext context);
        void OnSlowTapPerformed(InputAction.CallbackContext context);
        void OnSlowTapCancelled(InputAction.CallbackContext context);
    }
}