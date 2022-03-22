using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game_Manager;
using PlayerInfosAPI;
using TMPro;

public class CardButtonScript : MonoBehaviour
{
    public bool toggled;
    private Image image;
    public GameObject associatedGameObject;
    public Color32 activatedColor;
    public Color32 deactivatedColor;
    private TextMeshProUGUI buttonText;

    public void Start()
    {
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        image = gameObject.GetComponent<Image>();
        if (PlayerInfos.instance.SelectedActiveCard.Contains(associatedGameObject)) toggled = true;
        else if (PlayerInfos.instance.SelectedPassiveCard.Contains(associatedGameObject)) toggled = true;
        else toggled = false;
        if (toggled) image.color = activatedColor;
        else image.color = deactivatedColor;
        buttonText.text = associatedGameObject.name;
    }

    public void OnClick()
    {
        if(!toggled)
        {
            image.color = activatedColor;
            GameManagerAPI.instance.selectCard(associatedGameObject);
        }
        else
        {
            image.color = deactivatedColor;
            GameManagerAPI.instance.unSelectCard(associatedGameObject);
        }
        toggled = !toggled;
    }
}
