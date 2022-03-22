using CardSystem;
using Control.ActiveCardControl.ControlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control.ActiveCardControl
{
    public class ActiveCardControlRebind: MonoBehaviour
    {
        [SerializeField]private InputActionAsset inputAsset;
        private const string LinkedActionMap = "Player";
        private const string LinkedAction = "TriggerActiveCard";
        private PlayerInput playerInput;
        private const string TapString = "tap(duration={0})";
        private const string SlowTapString = "slowTap(duration={0})";
        private const string PreciseChargeString = "preciseCharge(pressDuration={0},releaseDuration={1})";


        private void Start()
        {
            CardSystemManager.instance.onSelectedCardChange += UpdateControl;
        }

        private void UpdateControl(ActiveCard activeCard)
        {
            var interactionString = "";
            var stackCount = 0;
            if (activeCard is ITappable tappable)
            {
                interactionString += ConstructTapString(tappable);
                stackCount += 1;
            }

            if (activeCard is IPreciseChargeable preciseChargeable)
            {
                if (stackCount > 0) interactionString += ",";
                interactionString += ConstructPreciseChargeString(preciseChargeable);
                stackCount += 1;
            }

            if (activeCard is ISlowTappable slowTappable)
            {
                if (stackCount > 0) interactionString += ",";
                interactionString += ConstructSlowTapString(slowTappable);
            }
            ChangeParam(interactionString);
        }
        private void ChangeParam(string interactionString)
        {
            inputAsset.FindActionMap(LinkedActionMap).FindAction(LinkedAction).
                ApplyBindingOverride(new InputBinding{overrideInteractions = interactionString});
        }

        private static string ConstructTapString(ITappable tappable)
        {
            return string.Format(TapString, tappable.tapTime);
        }

        private static string ConstructPreciseChargeString(IPreciseChargeable preciseChargeable)
        {
            return string.Format(PreciseChargeString, preciseChargeable.pressDuration,
                preciseChargeable.releaseDuration);
        }

        private static string ConstructSlowTapString(ISlowTappable slowTappable)
        {
            return string.Format(SlowTapString, slowTappable.slowTapTime);
        }

    }
    
}