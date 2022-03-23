using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class PreciseChargeInteraction : IInputInteraction
    {
        private double chargeStartTime;
        public float pressDuration;
        public float pressPoint;
        public float releaseDuration;

        static PreciseChargeInteraction()
        {
            InputSystem.RegisterInteraction<PreciseChargeInteraction>();
        }

        private float pressDurationOrDefault => pressDuration > 0.0 ? pressDuration : 1f;
        private float releaseDurationOrDefault => releaseDuration > 0.0 ? releaseDuration : 1f;
        private float pressPointOrDefault => pressPoint > 0 ? pressPoint : 0.8f;

        public void Process(ref InputInteractionContext context)
        {
            if (context.timerHasExpired)
            {
                context.Canceled();
                return;
            }

            if (context.isWaiting && context.ControlIsActuated(pressPointOrDefault))
            {
                chargeStartTime = context.time;
                context.Started();
                context.SetTimeout(pressDurationOrDefault + releaseDurationOrDefault + 0.00001f);
                return;
            }

            if (!context.isStarted || context.ControlIsActuated(pressPointOrDefault)) return;
            if (context.time - chargeStartTime <= pressDurationOrDefault)
                context.Canceled();
            else
                context.Performed();
        }

        public void Reset()
        {
            chargeStartTime = 0.0;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            // Will execute the static constructor as a side effect.
        }
    }
}