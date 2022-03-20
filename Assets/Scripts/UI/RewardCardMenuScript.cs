using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;
using UnityEngine.UI;
using TMPro;
using CardSystem;

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
    void Start()
    {
        index = 0;
        rewards = GameManagerAPI.instance.rewards;
        descriptionText = descriptionSection.GetComponentInChildren<TextMeshProUGUI>();
        imgComponent = cardSprite.GetComponent<Image>();
        update();
    }

    private void update()
    {
        displayedCard = Instantiate(rewards[index]);
        displayedCard.SetActive(true);
        imgComponent.sprite = displayedCard.GetComponent<Image>().sprite;
        if (displayedCard.GetComponent<ActiveCard>() != null)
        {
            Debug.Log(displayedCard.GetComponent<Card>().description);
            descriptionText.text = displayedCard.GetComponent<ActiveCard>().description;
        }
        else if (displayedCard.GetComponent<PassiveCard>() != null)
        {
            descriptionText.text = displayedCard.GetComponent<PassiveCard>().description;
        }
        else descriptionText.text = "Description not found !";
        displayedCard.SetActive(false);
        Destroy(displayedCard);
    }

    public void getThisCard()
    {
        GameManagerAPI.instance.unlockCard(rewards[index]);
        GameManagerAPI.instance.selectCard(rewards[index]);
    }

    public void next()
    {
        if(index < rewards.Count-1)
        {
            index++;
            update();
        }
    }

    public void previous()
    {
        if(index > 0)
        {
            index--;
            update();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
