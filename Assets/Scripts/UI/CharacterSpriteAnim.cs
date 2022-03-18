using System;
using DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterSpriteAnim : MonoBehaviour
    {
        private CharacterSpriteSetList characterSpriteSetList;
        private bool faded = false;
        [SerializeField] private string fadeAnimName;
        [SerializeField] private string restoreAnimName;
        private Animation anim;
        private Image characterSprite;

        private void Start()
        {
            characterSpriteSetList ??= DialogueAssetDatabase.instance.characterSpriteSetList;
            anim ??= GetComponent<Animation>();
            characterSprite ??= GetComponent<Image>();
        }

        public void SetSprite(DialogueItem dialogueItem)
        {
            characterSpriteSetList ??= DialogueAssetDatabase.instance.characterSpriteSetList;
            anim ??= GetComponent<Animation>();
            characterSprite ??= GetComponent<Image>();
            Debug.Log(dialogueItem.character+dialogueItem.emotion);
            characterSprite.sprite = characterSpriteSetList.GetSprite(dialogueItem.character, dialogueItem.emotion);
        }

        public void Fade()
        {
            if (!gameObject.activeInHierarchy || faded) return;
            anim ??= GetComponent<Animation>();
            anim.Play(fadeAnimName);
            faded = true;
        }

        public void Restore()
        {
            if (!gameObject.activeInHierarchy || !faded) return;
            anim ??= GetComponent<Animation>();
            anim.Play(restoreAnimName);
            faded = false;
        }
    }
}