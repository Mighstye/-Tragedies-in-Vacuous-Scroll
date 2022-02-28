using System;
using UnityEngine;

namespace BossBehaviour
{
    public class PhaseTimer : MonoBehaviour
    {
        public static PhaseTimer instance { get; private set; }
        public Action onPhaseTimeoutReached;

        //Display Action
        public Action onNeedTimeoutRefreshDisplay;
        public Action onNeedTimerRefreshDisplay;
        public Action onPhaseEndDisplay;

        public string phaseName { get; set; }
        [SerializeField] public float currentPhaseTimeout;
        [SerializeField] public float phaseTimer;

        private bool timeoutReached ;
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            timeoutReached = true;
        }

        public void SetUpTimer(float timeout, string pName="Unnamed Phase")
        {
            currentPhaseTimeout = timeout;
            phaseTimer = timeout;
            phaseName = pName;
            timeoutReached = false;
            onNeedTimeoutRefreshDisplay?.Invoke();
            onNeedTimerRefreshDisplay?.Invoke();
        }

        private void Update()
        {
            if (timeoutReached) return;
            phaseTimer -= Time.deltaTime;
            onNeedTimerRefreshDisplay?.Invoke();
            if (!(phaseTimer <= 0)) return;
            onPhaseTimeoutReached?.Invoke();
            onPhaseEndDisplay?.Invoke();
            timeoutReached = true;
        }
    }
}