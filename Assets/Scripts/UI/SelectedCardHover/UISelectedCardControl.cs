using CardSystem;
using Logic_System;
using UnityEngine;

namespace UI.SelectedCardHover
{
    public class UISelectedCardControl : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private UISelectedCardHover cardImageLight;
        [SerializeField] private UISelectedCardHover cardImageDark;
        [SerializeField] private CoolDownUI coolDownUI;
        private ActiveCard displayedCard;
        private Graze grazeRef;

        private void Start()
        {
            grazeRef = LogicSystemAPI.instance.graze;
            grazeRef.onNeedGrazeRefresh += () => { cardImageLight.Charge(grazeRef.GetSegment()); };
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("GrazeBox")) return;
            canvasGroup.alpha = 0.25f;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("GrazeBox")) return;
            canvasGroup.alpha = 1f;
        }

        public void UpdateSelectedCard(ActiveCard activeCard)
        {
            coolDownUI.trackedCard = activeCard;
            cardImageDark.UpdateSelectedCard(activeCard);
            cardImageLight.UpdateSelectedCard(activeCard);
            cardImageLight.Charge(grazeRef.GetSegment());
        }
    }
}