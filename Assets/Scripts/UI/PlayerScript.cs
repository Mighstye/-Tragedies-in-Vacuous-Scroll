using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private readonly Image[] hearts = new Image[8];

    // Start is called before the first frame update
    private void Start()
    {
        // We fill the SpriteRender table with all the concerned SpriteRender
        var i = 0;
        foreach(var heartUI in gameObject.GetComponentsInChildren<Image>())
        {
            hearts[i] = heartUI;
            i++;
        }
        LogicSystemAPI.instance.onNeedPlayerRefresh += RefreshDisplay;
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        for(var i = LogicSystemAPI.instance.getCurrentHealth(); i<hearts.Length; i++)
        {
            hearts[i].enabled = false;
        }
        for(var i =0;i< LogicSystemAPI.instance.getCurrentHealth(); i++)
        {
            hearts[i].enabled = true;
        }
    }
    
}
