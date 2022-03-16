using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.Localization;

namespace Logic_System
{
    public class PhaseStatistics
    {
        public bool hit { get; private set; }=false;
        public bool spellUse { get; private set; }=false;
        public int encounterCount { get; private set; } = 0;
        public int spellGetCount { get; private set; } = 0;

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