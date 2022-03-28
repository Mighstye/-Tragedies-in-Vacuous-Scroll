using System;
using System.Collections;
using UnityEngine;
using Utils.Events;

namespace BossBehaviour
{
    public class PhaseTimer : MonoBehaviour
    {
        [SerializeField] public float currentPhaseTimeout;
        [SerializeField] public float phaseTimer;
        private bool inCountDown;

        //Display Action
        public Action onNeedTimeoutRefreshDisplay;
        public Action onNeedTimerRefreshDisplay;
        public Action onPhaseEndDisplay;
        public Action onPhaseTimeoutReached;

        [SerializeField] private int tickCount = 5;
        [SerializeField] private GameEvent onFinalCountdownTick;

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
            if (!inCountDown && phaseTimer - tickCount < 0 && phaseName is not null)
            {
                inCountDown = true;
                StartCoroutine(CountDownTick());
            }
            if (!(phaseTimer <= 0)) return;
            onPhaseTimeoutReached?.Invoke();
            onPhaseEndDisplay?.Invoke();
            timeoutReached = true;
        }

        public void SetUpTimer(float timeout, string pName = null)
        {
            inCountDown = false;
            currentPhaseTimeout = timeout;
            phaseTimer = timeout;
            phaseName = pName;
            timeoutReached = false;
            onNeedTimeoutRefreshDisplay?.Invoke();
            onNeedTimerRefreshDisplay?.Invoke();
        }

        private IEnumerator CountDownTick()
        {
            for (var i = 0; i < tickCount; i++)
            {
                onFinalCountdownTick.Invoke();
                yield return new WaitForSeconds(1);
            }
        }
    }
}