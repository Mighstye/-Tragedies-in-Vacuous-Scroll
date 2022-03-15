using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class TemporarSpecialCard : MonoBehaviour
{
    [SerializeField] private Transform ActiveCardsContainer;
    [SerializeField] private ActiveCard SpecialCard;
    public void ActivateSpecialCard()
    {
        // Desactivation temporaire des active cards

        // Recuperation des active cards
        var cards = ActiveCardsContainer.gameObject.GetComponentsInChildren<ActiveCard>();
        foreach (var card in cards)
        {
            card.enabled = false;
        }

        // Activation de la special card
        Instantiate(SpecialCard);

        SpecialCard.transform.parent = ActiveCardsContainer.transform;
    }
}
