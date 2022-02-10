using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private SpriteRenderer[] hearts = new SpriteRenderer[8];

    // Start is called before the first frame update
    void Start()
    {
        // We fill the SpriteRender table with all the concerned SpriteRender
        for(int i=1; i<=hearts.Length; i++)
        {
            hearts[i-1] = GameObject.Find("heart" + i).GetComponent<SpriteRenderer>();
        }
        Health.instance.onNeedPlayerRefresh += () =>
        {
            refreshDisplay();
        };
    }

    private void refreshDisplay()
    {
        for(int i = Health.instance.get(); i<hearts.Length; i++)
        {
            hearts[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
