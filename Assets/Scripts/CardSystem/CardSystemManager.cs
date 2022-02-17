using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
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
        private float chargeTimer=0;
        private bool chargeStarted = false;
        

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            activeCardManager.RunTest();//TODO: This is for test => Delete
            if (activeCardManager.selectedCard == null)
            {
                selectedCardHover.gameObject.SetActive(false);
            }

            chargeStarted = false;

        }

        private void Update()
        {
            if(chargeStarted)chargeTimer += Time.deltaTime;
        }

        public void OnCardSwitch(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed||chargeStarted) return;
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

        public void OnTriggerActiveCard(InputAction.CallbackContext context)
        {
            var selected = activeCardManager.selectedCard;
            switch (context.action.phase)
            {
                case InputActionPhase.Performed when context.interaction is TapInteraction:
                    selected.OnTap(context);
                    break;
                case InputActionPhase.Performed when context.interaction is PressInteraction:
                    chargeStarted = true;
                    selected.OnCharge(context);
                    break;
                case InputActionPhase.Canceled when context.interaction is PressInteraction:
                    chargeStarted = false;
                    if (selected.chargeTime > chargeTimer)
                    {
                        selected.OnChargeFailed(context);
                        Debug.Log(chargeTimer);
                    }
                    else if (selected.releaseTime>0&&selected.releaseTime + selected.chargeTime < chargeTimer)
                    {
                        selected.OnOverCharge(context);
                        Debug.Log(chargeTimer);
                    }
                    else
                    {
                        selected.OnChargeComplete(context);
                        Debug.Log(chargeTimer);
                    }
                    chargeTimer = 0;
                    break;
            }
        }
    }
}