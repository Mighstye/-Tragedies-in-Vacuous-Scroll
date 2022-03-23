using DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterSpriteAnim : MonoBehaviour
    {
        [SerializeField] private string fadeAnimName;
        [SerializeField] private string restoreAnimName;
        private Animation anim;
        private Image characterSprite;
        private CharacterSpriteSetList characterSpriteSetList;
        private bool faded;

        private void Start()
        {
            characterSpriteSetList ??= DialogueAssetDatabase.instance.characterSpriteSetList;
            anim ??= GetComponent<Animation>();
            characterSprite ??= GetComponent<Image>();
        }

        public void SetSprite(string characterID, string emotion)
        {
            characterSpriteSetList ??= DialogueAssetDatabase.instance.characterSpriteSetList;
            anim ??= GetComponent<Animation>();
            characterSprite ??= GetComponent<Image>();
            characterSprite.sprite = characterSpriteSetList.GetSprite(characterID, emotion);
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