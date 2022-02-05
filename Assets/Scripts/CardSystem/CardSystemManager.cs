using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

namespace CardSystem
{
    public class CardSystemManager : MonoBehaviour
    {
        public static CardSystemManager instance { get; private set; }
        [SerializeField] private ActiveCardManager activeCardManager;
        [SerializeField] private PassiveCardManager passiveCardManager;
        [SerializeField] private SelectedCardHover selectedCardHover;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (activeCardManager.selectedCard == null)
            {
                selectedCardHover.gameObject.SetActive(false);
            }
            
        }

        public void OnCardSwitch(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            activeCardManager.SelectNext();
            if (activeCardManager.selectedCard == null)
            {
                selectedCardHover.gameObject.SetActive(false);
                return;
            }
            selectedCardHover.gameObject.SetActive(true);
            selectedCardHover.UpdateSelectedCard(activeCardManager.selectedCard);
        }

        public void AddActiveCard(ActiveCard card)
        {
            activeCardManager.Add(card);
        }
    }
}