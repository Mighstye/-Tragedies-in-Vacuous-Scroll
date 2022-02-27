using System;
using UnityEngine;

namespace BossBehaviour
{
    public class PhaseTimer : MonoBehaviour
    {
        public static PhaseTimer instance { get; private set; }
        public Action onPhaseTimeoutReached;
        public string phaseName { get; set; }
        [SerializeField] private float currentPhaseTimeout;
        [SerializeField] private float phaseTimer;

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
        }

        private void Update()
        {
            if (timeoutReached) return;
            phaseTimer -= Time.deltaTime;
            if (!(phaseTimer <= 0)) return;
            onPhaseTimeoutReached?.Invoke();
            timeoutReached = true;
        }
    }
}