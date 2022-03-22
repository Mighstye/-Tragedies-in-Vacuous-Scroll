using System.Collections;
using Logic_System;
using UnityEngine;

namespace UI.TopUIPanel
{
    public class TopPanelFade : MonoBehaviour
    {
        private BattleOutcome battleOutcomeRef;
        private CanvasGroup topPanel = null;

        [SerializeField] private float fadeSpeed = 0.01f;
        private void Start()
        {
            topPanel ??= GetComponent<CanvasGroup>();
            battleOutcomeRef = LogicSystemAPI.instance.battleOutcome;
            battleOutcomeRef.onPhaseStart += (s, statistics) =>
            {
                StartCoroutine(s is null ? Fade(false) : Fade(true));
            };
        }

        private IEnumerator Fade(bool fadeIn)
        {
            var vFadeSpeed = fadeIn ? fadeSpeed : -1 * fadeSpeed;
            topPanel.alpha=Mathf.Clamp(topPanel.alpha, 0.01f, 0.99f);
            while (topPanel.alpha is < 1 and > 0)
            {
                topPanel.alpha += vFadeSpeed;
                yield return null;
            }
        }
    }
}