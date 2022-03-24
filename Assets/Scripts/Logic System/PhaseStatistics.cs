using System;
using UnityEngine;

namespace Logic_System
{
    [Serializable]
    public class PhaseStatistics
    {
        [SerializeField] public string phaseID;
        [NonSerialized] public bool hit;
        [NonSerialized]public bool spellUse;
        [SerializeField] public int encounterCount;
        [SerializeField] public int spellGetCount;

        public void RecordSpellUse()
        {
            spellUse = true;
        }

        public void RecordHealthLost()
        {
            hit = true;
        }

        public void IncreaseEncounterCount()
        {
            encounterCount++;
        }

        public void IncreaseSpellGetCount()
        {
            spellGetCount++;
        }

        public bool SpellGet()
        {
            return !(spellUse || hit);
        }


        public override string ToString()
        {
            return "Hit=" + hit + "|Spell=" + spellUse + "|" + spellGetCount + "/" + encounterCount;
        }
    }
}