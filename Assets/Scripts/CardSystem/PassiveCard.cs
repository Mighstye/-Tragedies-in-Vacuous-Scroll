namespace CardSystem
{
    public class PassiveCard: Card
    {
        public string passiveDesc;

        public PassiveCard()
        {
            passiveDesc = "Passive Card Description";
            description = "Passive : +\n" + passiveDesc;
        }
    }
}