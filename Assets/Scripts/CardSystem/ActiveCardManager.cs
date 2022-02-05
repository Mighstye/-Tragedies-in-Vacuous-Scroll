using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class ActiveCardManager : MonoBehaviour
    {
        public List<ActiveCard> currentActiveCards { get; private set; }
        [SerializeField] private Transform cardContainer;
        public ActiveCard selectedCard { get; private set; }
        private int selectedCardIndex;

        private void Start()
        {
            currentActiveCards = new List<ActiveCard>();
            //TODO: following code is for testing, delete the fragment afterwards
            var cards = cardContainer.gameObject.GetComponentsInChildren<ActiveCard>();
            foreach (var card in cards)
            {
                Debug.Log(card.gameObject.name);
                currentActiveCards.Add(card);
            }
            //END OF FRAGMENT
            if (currentActiveCards.Count <= 0) return;
            selectedCardIndex = 0;
            selectedCard = currentActiveCards[selectedCardIndex];
        }

        public void Add(ActiveCard activeCard)
        {
            currentActiveCards.Add(activeCard);
            activeCard.gameObject.transform.SetParent(cardContainer,worldPositionStays:false) ;
        }

        public void Remove(ActiveCard activeCard)
        {
            currentActiveCards.Remove(activeCard);
        }

        public void Arrange()
        {
            var index = 0;
            foreach (var card in currentActiveCards)
            {
                card.transform.SetSiblingIndex(index);
                index++;
            }
        }

        public void SelectNext()
        {
            if (currentActiveCards.Count < 1) return;
            selectedCardIndex++;
            if (selectedCardIndex >= currentActiveCards.Count) selectedCardIndex = 0;
            selectedCard = currentActiveCards[selectedCardIndex];
        }
        
        
    }
}