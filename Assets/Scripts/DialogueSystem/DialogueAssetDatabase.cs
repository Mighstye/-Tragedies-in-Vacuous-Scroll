using System;
using UnityEngine;
using Utils;

namespace DialogueSystem
{
    public class DialogueAssetDatabase : Singleton<DialogueAssetDatabase>
    {
        public BubbleStyleSet bubbleStyleSet;
        public CharacterSpriteSetList characterSpriteSetList;
    }
}