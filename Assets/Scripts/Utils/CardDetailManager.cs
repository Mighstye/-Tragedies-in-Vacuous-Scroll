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
    public class CardDetailManager : MonoBehaviour
    {
        public static CardDetailManager instance { get; private set; }
        [SerializeField] private CardDetailUI cardDetailUI;
        [SerializeField] private CircularCardListBank cardListBank;
        [SerializeField] private GameObject Menu;

        [SerializeField] private CircularCardListBank listBank;
        
        private UIManager UiManager;

        private void Awake()
        {
            instance ??= this;
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        

        public void Start()
        {
            this.gameObject.SetActive(true);
            cardListBank.Init(new List<Card>(
                GameManagerAPI.instance.rewards.Select(o=>o.GetComponent<Card>())));
            UiManager = Menu.GetComponent<UIManager>();
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
            UiManager.Continue();
            this.gameObject.SetActive(false);
        }
    }
}