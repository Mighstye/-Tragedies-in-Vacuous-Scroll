using System;
using System.Collections;
using DG.Tweening;
using Logic_System;
using UnityEngine;

namespace UI
{
    public class SpellBreak : MonoBehaviour
    {
        private BattleOutcome battleOutcome;
        private RectTransform panelRoot;
        [SerializeField] private float stayTime = 2;
        [SerializeField] private Vector2 centralPoint;
        [SerializeField] private Vector2 startPoint;
        [SerializeField] private Vector2 endPoint;
        [SerializeField] private float tweenTime=1;

        
        private void Start()
        {
            panelRoot = GetComponent<RectTransform>();
            battleOutcome = LogicSystemAPI.instance.battleOutcome;
            panelRoot.anchoredPosition = startPoint;
            battleOutcome.onPhaseEnd += (s, statistics) =>
            {
                if (s is not null && statistics.SpellGet())
                {
                    TweenIn();
                }
            };
        }
        
        private void TweenIn()
        {
            panelRoot.anchoredPosition = startPoint;
            Tween(centralPoint).OnComplete(() => { StartCoroutine(Stay());});
        }

        private IEnumerator Stay()
        {
            yield return new WaitForSeconds(stayTime);
            TweenOut();
        }
        
        private void TweenOut()
        {
            Tween(endPoint);
        }

        private Tweener Tween(Vector2 posEnd)
        {
            return panelRoot.DOAnchorPos(posEnd, tweenTime);
        }

        #region EditorHelper
        [ContextMenu("Set Central Position")]
        public void CopyCentralPosition()
        {
                     
            centralPoint = GetComponent<RectTransform>().anchoredPosition;
        }
                 
                 
        [ContextMenu("Set Start Position")]
        public void CopyStartPosition()
        {
            startPoint = GetComponent<RectTransform>().anchoredPosition;
        }
                 
        [ContextMenu("Set End Position")]
        public void CopyEndPosition()
        {
            endPoint = GetComponent<RectTransform>().anchoredPosition;
        }
        
        #endregion
    }
}