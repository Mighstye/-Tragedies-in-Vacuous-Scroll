using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private SpriteRenderer[] hearts = new SpriteRenderer[8];
    private int curIndex;
    private int tick; // Will be removed

    // Start is called before the first frame update
    void Start()
    {
        // We fill the SpriteRender table with all the concerned SpriteRender
        for(int i=1; i<=hearts.Length; i++)
        {
            hearts[i-1] = GameObject.Find("heart" + i).GetComponent<SpriteRenderer>();
            curIndex = hearts.Length-1;
            tick = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Only for showing how everything work
        // Next, we will only refresh the display with Youmu informations
        tick++;
        if(tick == 100)
        {
            tick = 0;
            if(curIndex==-1)
            {
                curIndex = hearts.Length - 1;
                foreach(SpriteRenderer element in hearts)
                {
                    element.enabled = true;
                }
            }
            else
            {
                hearts[curIndex].enabled = false;
                curIndex--;
            }
        }
    }
}
