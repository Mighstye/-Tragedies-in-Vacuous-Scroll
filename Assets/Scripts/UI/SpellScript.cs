using Logic_System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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
            LogicSystemAPI.instance.onNeedSpellRefresh += RefreshDisplay;
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            for(var i = LogicSystemAPI.instance.getCurrentSpellAmount(); i<spells.Length; i++)
            {
                spells[i].enabled = false;
            }

            for (var i = 0; i < LogicSystemAPI.instance.getCurrentSpellAmount(); i++)
            {
                spells[i].enabled = true;
            }
        }

    }
}
