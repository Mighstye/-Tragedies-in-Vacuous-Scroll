using System;
using System.Collections.Generic;
using CardSystem;
using UI;
using UnityEngine;

namespace Utils
{
    public class CardDetailManager : MonoBehaviour
    {
        public static CardDetailManager instance { get; private set; }
        [SerializeField] private CardDetailUI cardDetailUI;
        [SerializeField] private CircularCardListBank cardListBank;

        private void Awake()
        {
            instance ??= this;
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        public void Init(List<Card> cards)
        {
            cardListBank.Init(cards);
        }

        public Card RetrieveSelectedCard()
        {
            return cardDetailUI.currentSelectedCard;
        }
    }
}