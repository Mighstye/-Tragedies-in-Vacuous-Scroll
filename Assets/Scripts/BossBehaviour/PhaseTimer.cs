using System;
using UnityEngine;

namespace BossBehaviour
{
    public class PhaseTimer : MonoBehaviour
    {
        [SerializeField] public float currentPhaseTimeout;
        [SerializeField] public float phaseTimer;

        //Display Action
        public Action onNeedTimeoutRefreshDisplay;
        public Action onNeedTimerRefreshDisplay;
        public Action onPhaseEndDisplay;
        public Action onPhaseTimeoutReached;

        private bool timeoutReached;
        public static PhaseTimer instance { get; private set; }

        public string phaseName { get; set; }

        private void Awake()
        {
            instance ??= this;
            if (instance != this) Destroy(gameObject);
        }

        private void Start()
        {
            timeoutReached = true;
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

        public void SetUpTimer(float timeout, string pName = "Unnamed Phase")
        {
            currentPhaseTimeout = timeout;
            phaseTimer = timeout;
            phaseName = pName;
            timeoutReached = false;
            onNeedTimeoutRefreshDisplay?.Invoke();
            onNeedTimerRefreshDisplay?.Invoke();
        }
    }
}