using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;
using UnityEngine.UI;
using TMPro;
using CardSystem;

[Obsolete]
public class RewardCardMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject descriptionSection;
    [SerializeField] private GameObject cardSprite;
    private TextMeshProUGUI descriptionText;
    private List<GameObject> rewards;
    private Image imgComponent;
    private GameObject displayedCard;
    private int index;

    // Start is called before the first frame update
    private void Start()
    {
        index = 0;
        rewards = GameManagerAPI.instance.rewards;
        descriptionText = descriptionSection.GetComponentInChildren<TextMeshProUGUI>();
        imgComponent = cardSprite.GetComponent<Image>();
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        displayedCard = Instantiate(rewards[index]);
        displayedCard.SetActive(true);
        imgComponent.sprite = displayedCard.GetComponent<Image>().sprite;
        if (displayedCard.GetComponent<ActiveCard>() != null)
        {
            
        }
        else if (displayedCard.GetComponent<PassiveCard>() != null)
        {
            
        }
        else descriptionText.text = "Description not found !";
        displayedCard.SetActive(false);
        Destroy(displayedCard);
    }

    public void GetThisCard()
    {
        GameManagerAPI.instance.unlockCard(rewards[index]);
        GameManagerAPI.instance.selectCard(rewards[index]);
    }

    public void Next()
    {
        if(index < rewards.Count-1)
        {
            index++;
            UpdateMenu();
        }
    }

    public void Previous()
    {
        if (index <= 0) return;
        index--;
        UpdateMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
