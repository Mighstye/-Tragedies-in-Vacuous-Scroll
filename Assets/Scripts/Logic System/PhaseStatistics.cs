namespace Logic_System
{
    public class PhaseStatistics
    {
        public bool hit { get; private set; }
        public bool spellUse { get; private set; }
        public int encounterCount { get; private set; }
        public int spellGetCount { get; private set; }

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