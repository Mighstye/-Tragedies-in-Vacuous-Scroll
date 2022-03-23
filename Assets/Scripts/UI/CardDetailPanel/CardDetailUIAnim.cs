using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class CardDetailUIAnim : MonoBehaviour
    {
        private RectTransform panelRoot;
        private CanvasGroup canvasGroup;
        [SerializeField] private Vector2 centralPoint;
        [SerializeField] private Vector2 startPoint;
        [SerializeField] private Vector2 endPoint;
        [SerializeField] private float tweenTime=1;
        private void OnEnable()
        {
            panelRoot = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            panelRoot.anchoredPosition = startPoint;
            Tween(centralPoint,1);
        }
        
        private void OnDisable()
        {
          Tween(endPoint,0);
        }

        private void Tween(Vector2 posEnd, float alphaEnd)
        {
              panelRoot.DOAnchorPos(posEnd, tweenTime);
              DOTween.To(()=>canvasGroup.alpha, (a)=>
              {
                  canvasGroup.alpha=a;
              }, alphaEnd, tweenTime);
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