using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public struct CharacterSpriteDictItem
    {
        [SerializeField] public string characterID;
        [SerializeField] public CharacterSpriteSet spriteSet;
    }

    [CreateAssetMenu(fileName = "CharacterSpriteSetList", menuName = "CharacterSpriteSetList", order = 1)]
    public class CharacterSpriteSetList : ScriptableObject
    {
        public List<CharacterSpriteDictItem> items;

        public Sprite GetSprite(string character, string emotionID)
        {
            var spriteSetItem = items.Where(item => item.characterID == character).Select(item => item.spriteSet)
                .FirstOrDefault();
            return spriteSetItem != null ? spriteSetItem.GetSpriteByEmotion(emotionID) : null;
        }
    }
}