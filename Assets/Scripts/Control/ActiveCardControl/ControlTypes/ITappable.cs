using UnityEngine.InputSystem;

namespace Control.ActiveCardControl.ControlTypes
{
    public interface ITappable : IControlType
    {
        float tapTime { get; set; }
        void OnTapPerformed(InputAction.CallbackContext context);
        void OnTapCancelled(InputAction.CallbackContext context);
    }
}