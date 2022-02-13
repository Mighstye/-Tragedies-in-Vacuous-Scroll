using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    private readonly Image[] spells = new Image[8];

    // Start is called before the first frame update
    private void Start()
    {
        var i = 0;
        foreach(var spellUI in gameObject.GetComponentsInChildren<Image>())
        {
            spells[i] = spellUI;
            i++;
        }
        Spell.instance.onNeedSpellRefresh += RefreshDisplay;
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        for(var i = Spell.instance.currentSpellAmount; i<spells.Length; i++)
        {
            spells[i].enabled = false;
        }

        for (var i = 0; i < Spell.instance.currentSpellAmount; i++)
        {
            spells[i].enabled = true;
        }
    }

}
