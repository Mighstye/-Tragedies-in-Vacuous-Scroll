using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    private SpriteRenderer[] spells = new SpriteRenderer[8];

    // Start is called before the first frame update
    void Start()
    {
        for(int i=1; i<=spells.Length; i++)
        {
            // We fill the SpriteRender table with all the concerned SpriteRender
            spells[i - 1] = GameObject.Find("spell" + i).GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
