using Logic_System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpellScript : MonoBehaviour
    {
        private readonly Image[] spells = new Image[8];

        private Spell spellRef;

        // Start is called before the first frame update
        private void Start()
        {
            spellRef = LogicSystemAPI.instance.Spell;

            var i = 0;
            foreach(var spellUI in gameObject.GetComponentsInChildren<Image>())
            {
                spells[i] = spellUI;
                i++;
            }
            spellRef.onNeedSpellRefresh += RefreshDisplay;
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            for(var i = spellRef.currentSpellAmount; i<spells.Length; i++)
            {
                spells[i].enabled = false;
            }

            for (var i = 0; i < spellRef?.currentSpellAmount; i++)
            {
                spells[i].enabled = true;
            }
        }

    }
}
