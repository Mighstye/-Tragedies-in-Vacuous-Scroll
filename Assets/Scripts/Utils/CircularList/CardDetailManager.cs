using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardSystem;
using DG.Tweening;
using GameManager;
using Logic_System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utils.Events;

namespace Utils
{
    public class CardDetailManager : Singleton<CardDetailManager>
    {
        private const string ActionMap = "CardDropSelection";

        [SerializeField] private float entryDelay = 2.5f;
        [SerializeField] private GameObject uiRoot;
        [SerializeField] private CardDetailUI cardDetailUI;
        [SerializeField] private CircularCardListBank cardListBank;
        [SerializeField] private CircularCardListBank listBank;
        [SerializeField] private GameEvent onCardFocusChange;
        [SerializeField] private GameEvent onCardConfirm;

        private RewardSystem rewardSystem;
        private UIManager uiManager;

        protected override void Awake()
        {
            base.Awake();
            uiRoot.SetActive(false);
        }

        public void OnBossDefeatWrapper()
        {
            StartCoroutine(OnBossDefeat());
        }

        private IEnumerator OnBossDefeat()
        {
            rewardSystem = LogicSystemAPI.instance.rewardSystem;
            Init(rewardSystem.GetReward());
            yield return new WaitForSeconds(entryDelay);
            uiRoot.SetActive(true);
            //uiManager = menu.GetComponent<UIManager>();
        }

       

        private void Init(List<Card> cards)
        {
            ControlManager.instance.SwitchMap(ActionMap);
            cardListBank.Init(cards);
        }

        private Card RetrieveSelectedCard()
        {
            return cardListBank.selectedCard;
        }

        public void PassSelectedCard(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            onCardConfirm.Invoke();
            GameManagerAPI.instance.SelectCard(RetrieveSelectedCard().gameObject); //TODO: refactor this
            //uiManager.Continue();
            ControlManager.instance.SwitchToPlayer();
            gameObject.SetActive(false);
        }

        public void MoveSelection(InputAction.CallbackContext context)
        {
            listBank.MoveItem(context);
            onCardFocusChange.Invoke();
        }
    }
}