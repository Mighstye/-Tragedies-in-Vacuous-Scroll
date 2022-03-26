using UnityEngine;

namespace CardSystem.DataContainers
{
    [CreateAssetMenu(fileName = "CardLocMeta", menuName = "Card Meta/Card Loc Meta", order = 0)]
    public class CardLocalizableMetadata : ScriptableObject
    {
        public string cardName;
        [TextArea] public string effectText;
        [TextArea] public string loreText;
    }
}