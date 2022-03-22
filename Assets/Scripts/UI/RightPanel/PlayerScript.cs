using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logic_System;

public class PlayerScript : MonoBehaviour
{

    private readonly Image[] hearts = new Image[8];

    private Health healthRef;

    // Start is called before the first frame update
    private void Start()
    {
        healthRef = LogicSystemAPI.instance.health;

        // We fill the SpriteRender table with all the concerned SpriteRender
        var i = 0;
        foreach(var heartUI in gameObject.GetComponentsInChildren<Image>())
        {
            hearts[i] = heartUI;
            i++;
        }
        healthRef.onNeedPlayerRefresh += RefreshDisplay;
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        for(var i = healthRef.currentHealth; i<hearts.Length; i++)
        {
            hearts[i].enabled = false;
        }
        for(var i =0;i< healthRef?.currentHealth; i++)
        {
            hearts[i].enabled = true;
        }
    }
    
}
