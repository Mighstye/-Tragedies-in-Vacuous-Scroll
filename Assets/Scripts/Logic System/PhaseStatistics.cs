namespace Logic_System
{
    public class PhaseStatistics
    {
        private bool hit =false;
        private bool spellUse =false;
        private int encounterCount = 0;
        private int spellGetCount = 0;

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