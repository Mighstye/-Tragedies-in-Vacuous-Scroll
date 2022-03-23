using System;
using System.Collections.Generic;
using CardSystem;
using UnityEngine;

namespace Utils
{
    [Obsolete]
    public class TestAPI : MonoBehaviour
    {
        public Card cardPrefab;
        public List<Card> cards;
        public CircularCardListBank circularList;

        public GameObject detailPanel;

        private void Start()
        {
            for (var i = 0; i < 4; i++)
            {
                var c = Instantiate(cardPrefab);
                c.gameObject.name = i.ToString();
                cards.Add(c);
            }

            detailPanel.SetActive(true);
            circularList.Init(cards);
        }
    }
}