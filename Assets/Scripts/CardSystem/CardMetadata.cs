using UnityEngine;

namespace CardSystem
{
    public enum CardType
    {
        Active,
        Passive
    }
    [CreateAssetMenu(fileName = "CardMeta", menuName = "CardMeta", order = 0)]
    public class CardMetadata : ScriptableObject
    {
        public CardType cardType;
        public int cost;
        public string cardName;
        [TextArea] public string effectText;
        [TextArea] public string loreText;
    }
}