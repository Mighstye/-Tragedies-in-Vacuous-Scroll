using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class TemporarSpecialCard : MonoBehaviour
{
    [SerializeField] private Transform ActiveCardsContainer;
    [SerializeField] private ActiveCard SpecialCard;
    private void Start()
    {
        // Desactivation temporaire des active cards

        // Recuperation des active cards
        var cards = ActiveCardsContainer.gameObject.GetComponentsInChildren<ActiveCard>();
        foreach (var card in cards)
        {
            card.gameObject.SetActive(false);
        }

        // Activation de la special card
        SpecialCard.gameObject.SetActive(true);
    }
}
