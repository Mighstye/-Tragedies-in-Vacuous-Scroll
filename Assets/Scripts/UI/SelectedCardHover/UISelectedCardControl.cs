using CardSystem;
using Logic_System;
using UnityEngine;

namespace UI.SelectedCardHover
{
    public class UISelectedCardControl : MonoBehaviour
    {
        [SerializeField]private UISelectedCardHover cardImageLight;
        [SerializeField] private UISelectedCardHover cardImageDark;
        private Graze grazeRef;

        private void Start()
        {
            grazeRef = LogicSystemAPI.instance.graze;
            grazeRef.onNeedGrazeRefresh += () =>
            {
                cardImageLight.Charge(grazeRef.GetSegment());
            };
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("GrazeBox")) return;
            cardImageLight.Fade();
            cardImageDark.Fade();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("GrazeBox")) return;
            cardImageLight.Restore();
            cardImageDark.Restore();
        }

        public void UpdateSelectedCard(ActiveCard activeCard)
        {
            cardImageDark.UpdateSelectedCard(activeCard);
            cardImageLight.UpdateSelectedCard(activeCard);
            cardImageLight.Charge(grazeRef.GetSegment());
        }
        
    }
}
