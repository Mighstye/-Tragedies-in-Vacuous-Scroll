using Logic_System;
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
        public float coolDown;
        public CardType cardType;
        public DropDeckType defaultDropType;
        public Sprite cardSprite;
        public VideoClip demoVideo;
    }
}