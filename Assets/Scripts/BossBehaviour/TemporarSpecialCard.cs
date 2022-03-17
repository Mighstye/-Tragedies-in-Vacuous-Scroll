using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class TemporarSpecialCard : MonoBehaviour
{
    [SerializeField] private Transform ActiveCardsContainer;
    [SerializeField] private ActiveCard SpecialCard;
    [SerializeField] private GameObject ActiveCardManagerObj;
    private ActiveCardManager ACM;
    private void Start()
    {
        ACM = ActiveCardManagerObj.GetComponent<ActiveCardManager>();
        // Recuperation des active cards
        var cards = ActiveCardsContainer.gameObject.GetComponentsInChildren<ActiveCard>();
        foreach (var card in cards)
        {
            card.gameObject.SetActive(false);
            ACM.Remove(card);
        }

        // Activation de la special card
        SpecialCard.gameObject.SetActive(true);
        ACM.Add(SpecialCard);
    }
}
