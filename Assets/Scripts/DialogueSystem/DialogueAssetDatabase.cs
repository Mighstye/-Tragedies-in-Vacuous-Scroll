using System;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueAssetDatabase : MonoBehaviour
    {
        public static DialogueAssetDatabase instance { get; private set; }

        public BubbleStyleSet bubbleStyleSet;
        public CharacterSpriteSetList characterSpriteSetList;
        private void Awake()
        {
            instance ??= this;
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        
        
    }
}