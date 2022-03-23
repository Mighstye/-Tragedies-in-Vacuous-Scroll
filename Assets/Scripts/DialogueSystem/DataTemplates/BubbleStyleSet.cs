using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public struct BubbleStyleSetItem
    {
        [SerializeField] public string emotion;
        [SerializeField] public Sprite sprite;
    }

    [CreateAssetMenu(fileName = "BubbleStyleSet", menuName = "BubbleStyleSet", order = 0)]
    public class BubbleStyleSet : ScriptableObject
    {
        public List<BubbleStyleSetItem> items = new();

        public Sprite GetSpriteByEmotion(string emotion)
        {
            emotion ??= "default";
            return items.Where(item => item.emotion == emotion).Select(item => item.sprite).FirstOrDefault();
        }
    }
}