using UnityEngine;
using UnityEngine.Video;

namespace CardSystem
{
    public enum CardType
    {
        Active,
        Passive
    }

    [CreateAssetMenu(fileName = "cardCommonMeta", menuName = "CardCommonMeta", order = 0)]
    public class CardCommonMetadata : ScriptableObject
    {
        public string key;
        public int cost;
        public CardType cardType;
        public Sprite cardSprite;
        public VideoClip demoVideo;
    }
}