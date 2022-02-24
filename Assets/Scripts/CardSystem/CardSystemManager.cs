using System;
using Control;
using Control.ActiveCardControl.ControlTypes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Debug = UnityEngine.Debug;

namespace CardSystem
{
    public class CardSystemManager : MonoBehaviour
    {
        public static CardSystemManager instance { get; private set; }
        [SerializeField] private ActiveCardManager activeCardManager;
        [SerializeField] private PassiveCardManager passiveCardManager;
        [SerializeField] private SelectedCardHover selectedCardHover;
        private float chargeTimer = 0;
        private bool chargeStarted = false;
        public Action<ActiveCard> onSelectedCardChange;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            activeCardManager.RunTest(); //TODO: This is for test => Delete
            if (activeCardManager.selectedCard == null)
            {
                selectedCardHover.gameObject.SetActive(false);
            }
            else
            {
                selectedCardHover.UpdateSelectedCard(activeCardManager.selectedCard);
                onSelectedCardChange?.Invoke(activeCardManager.selectedCard);
            }

            chargeStarted = false;


        }

        private void Update()
        {
            if (chargeStarted) chargeTimer += Time.deltaTime;
        }

        public void OnCardSwitch(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed || chargeStarted) return;
            activeCardManager.SelectNext();
            if (activeCardManager.selectedCard == null)
            {
                selectedCardHover.gameObject.SetActive(false);
                return;
            }
            selectedCardHover.gameObject.SetActive(true);
            selectedCardHover.UpdateSelectedCard(activeCardManager.selectedCard);
            onSelectedCardChange?.Invoke(activeCardManager.selectedCard);
        }

        public void AddActiveCard(ActiveCard card)
        {
            activeCardManager.Add(card);
        }

        public void OnTriggerActiveCard(InputAction.CallbackContext context)
        {
            var activeCard = activeCardManager.selectedCard;
            switch (context.action.phase)
            {
                case InputActionPhase.Performed when context.interaction is TapInteraction:
                    (activeCard as ITappable)?.OnTapPerformed(context);
                    break;
                case InputActionPhase.Canceled when context.interaction is TapInteraction:
                    (activeCard as ITappable)?.OnTapCancelled(context);
                    break;
                case InputActionPhase.Started when context.interaction is PreciseChargeInteraction:
                    (activeCard as IPreciseChargeable)?.OnPreciseChargeStarted(context);
                    break;
                case InputActionPhase.Performed when context.interaction is PreciseChargeInteraction:
                    (activeCard as IPreciseChargeable)?.OnPreciseChargePerformed(context);
                    break;
                case InputActionPhase.Canceled when context.interaction is PreciseChargeInteraction:
                    (activeCard as IPreciseChargeable)?.OnPreciseChargeCancelled(context);
                    break;
                case InputActionPhase.Performed when context.interaction is SlowTapInteraction:
                    (activeCard as ISlowTappable)?.OnSlowTapPerformed(context);
                    break;
                case InputActionPhase.Canceled when context.interaction is SlowTapInteraction:
                    (activeCard as ISlowTappable)?.OnSlowTapCancelled(context);
                    break;
                case InputActionPhase.Started when context.interaction is SlowTapInteraction:
                    (activeCard as ISlowTappable)?.OnSlowTapStarted(context);
                    break;

            }
        }
    }
}