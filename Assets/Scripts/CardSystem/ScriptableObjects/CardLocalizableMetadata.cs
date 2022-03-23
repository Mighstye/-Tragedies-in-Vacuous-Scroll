using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "CardLocMeta", menuName = "CardLocMeta", order = 0)]
    public class CardLocalizableMetadata : ScriptableObject
    {
        public string cardName;
        [TextArea] public string effectText;
        [TextArea] public string loreText;
    }
}