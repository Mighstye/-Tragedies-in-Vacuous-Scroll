using System;
using Control;
using Control.ActiveCardControl;
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
        [SerializeField] public ActiveCardManager activeCardManager;
        [SerializeField] public PassiveCardManager passiveCardManager;
        [SerializeField] public UISelectedCardControl selectedCardHover;
        
        public Action<ActiveCard> onSelectedCardChange;

        private bool isFirstFrame = true;


        private void Awake()
        {
            instance ??= this;
            if (instance != this)
            {
                Destroy(gameObject);
            }
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
            }
        }

        private void Update()
        {
            if (!isFirstFrame) return;
            onSelectedCardChange?.Invoke(activeCardManager.selectedCard);
            isFirstFrame = false;
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