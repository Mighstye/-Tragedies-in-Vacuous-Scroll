using System;
using System.Collections.Generic;
using System.Linq;
using CardSystem;
using Game_Manager;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public class CardDetailManager : Singleton<CardDetailManager>
    {
        [SerializeField] private CardDetailUI cardDetailUI;
        [SerializeField] private CircularCardListBank cardListBank;
        [SerializeField] private GameObject Menu;

        [SerializeField] private CircularCardListBank listBank;
        
        private UIManager uiManager;


        public void Start()
        {
            this.gameObject.SetActive(true);
            cardListBank.Init(new List<Card>(
                GameManagerAPI.instance.rewards.Select(o=>o.GetComponent<Card>())));
            uiManager = Menu.GetComponent<UIManager>();
        }

        public void Init(List<Card> cards)
        {
            cardListBank.Init(cards);
        }

        public Card RetrieveSelectedCard()
        {
            return cardListBank.selectedCard;
        }

        public void PassSelectedCard(InputAction.CallbackContext context)
        {
            if (context.phase is not InputActionPhase.Performed) return;
            GameManagerAPI.instance.selectCard(RetrieveSelectedCard().gameObject);
            uiManager.Continue();
            this.gameObject.SetActive(false);
        }
    }
}