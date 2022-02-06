using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    private SpriteRenderer[] spells = new SpriteRenderer[8];
    private int curIndex;
    private int tick;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=1; i<=spells.Length; i++)
        {
            // We fill the SpriteRender table with all the concerned SpriteRender
            spells[i - 1] = GameObject.Find("spell" + i).GetComponent<SpriteRenderer>();
            curIndex = spells.Length - 1;
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
            if(curIndex == -1)
            {
                curIndex = spells.Length - 1;
                foreach(SpriteRenderer element in spells)
                {
                    element.enabled = true;
                }
            }
            else
            {
                spells[curIndex].enabled = false;
                curIndex--;
            }
        }
    }
}
