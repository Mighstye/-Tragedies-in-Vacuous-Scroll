using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class TemporarSpecialCard : MonoBehaviour
{
    [SerializeField] private Transform ActiveCardsContainer;
    [SerializeField] private GameObject SpecialCard;
    [SerializeField] private GameObject ActiveCardManagerGameObject;
    [SerializeField] private GameObject SelectedCardControl;
    private UISelectedCardControl UISCC;
    private ActiveCardManager ACM;
    private void Start()
    {
        UISCC = SelectedCardControl.GetComponent<UISelectedCardControl>();
        ACM = ActiveCardManagerGameObject.GetComponent<ActiveCardManager>();
        // Desactivation des actives cards
        foreach (Transform child in ActiveCardsContainer)
        {
            ACM.Remove(child.gameObject.GetComponent<ActiveCard>());
            child.gameObject.SetActive(false);;
        }

        //Activation de la special cards
        SpecialCard.transform.SetParent(ActiveCardsContainer, false);
        SpecialCard.SetActive(true);
        ACM.Add(SpecialCard.GetComponent<ActiveCard>());
        ACM.SelectNext();
        UISCC.UpdateSelectedCard(SpecialCard.GetComponent<ActiveCard>());
    }
}
