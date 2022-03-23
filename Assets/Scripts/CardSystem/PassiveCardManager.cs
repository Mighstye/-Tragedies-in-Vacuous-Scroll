using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class PassiveCardManager : MonoBehaviour
    {
        [SerializeField] private Transform cardContainer;
        public List<PassiveCard> currentPassiveCards { get; private set; }

        private void Start()
        {
            currentPassiveCards = new List<PassiveCard>();
        }

        public void Add(PassiveCard passiveCard)
        {
            currentPassiveCards.Add(passiveCard);
            passiveCard.gameObject.transform.parent = cardContainer;
        }

        public void Remove(PassiveCard passiveCard)
        {
            currentPassiveCards.Remove(passiveCard);
        }
    }
}