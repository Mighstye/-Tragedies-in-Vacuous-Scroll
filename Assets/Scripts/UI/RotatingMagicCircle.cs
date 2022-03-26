using System;
using BossBehaviour;
using Control;
using DG.Tweening;
using Logic_System;
using UnityEngine;

namespace UI
{
    public class RotatingMagicCircle : MonoBehaviour
    {
        private RectTransform rectTransform;
        private BattleOutcome outcome;
        [SerializeField] private float startingXRotation = 30;
        [SerializeField] private float rotationSpeed = 1;
        private const float XRotationMultiplier = 0.2f;
        private bool inTransition = true;
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.localRotation*=Quaternion.Euler(startingXRotation,0,0);
            rectTransform.localScale=Vector3.zero;
            outcome = LogicSystemAPI.instance.battleOutcome;
            outcome.onPhaseStart += (s, statistics) =>
            {
                if (s is not null) StartFadeIn();
            };
            outcome.onPhaseEnd += (s, statistics) =>
            {
                if (s is not null) StartFadeOut();
            };
        }

        private void StartFadeIn()
        {
            inTransition = true;
            rectTransform.DOScale(Vector3.one * 2, 1).OnComplete(() => { inTransition = false; });
        }

        private void StartFadeOut()
        {
            rectTransform.DOScale(Vector3.zero, 0.5f);
        }

        private void FixedUpdate()
        { 
            transform.position = BossBehaviourSystemProxy.instance.bossController.transform.position;
            if (inTransition) return;
            var localRotation = rectTransform.localRotation;
            localRotation *= Quaternion.Euler(rotationSpeed*XRotationMultiplier,0,rotationSpeed);
            rectTransform.localRotation = localRotation;
      
        }
    }
}