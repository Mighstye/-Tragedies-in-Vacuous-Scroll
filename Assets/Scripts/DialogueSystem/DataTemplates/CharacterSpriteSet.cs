using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public struct CharacterSpriteSetItem
    {
        [SerializeField] public string emotion;
        [SerializeField] public Sprite sprite;
    }


    [CreateAssetMenu(fileName = "CharacterSpriteSet", menuName = "Dialogue/Character Sprite Set", order = 3)]
    public class CharacterSpriteSet : ScriptableObject
    {
        public List<CharacterSpriteSetItem> items;

        public Sprite GetSpriteByEmotion(string emotion)
        {
            emotion ??= "default";
            return items.Where(spriteSet => spriteSet.emotion == emotion).Select(item => item.sprite)
                .FirstOrDefault();
        }
    }
}