using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public enum HideStyle
    {
        Fade,
        Hide
    }
    public class DialogueUI : MonoBehaviour
    {
        private BubbleStyleSet bubbleStyleSet;


        [SerializeField] private CharacterSpriteAnim characterSpriteAnim;
        [SerializeField] private GameObject dialogueUIRoot;
        [SerializeField] private TextMeshProUGUI characterLabel;
        [SerializeField] private Image dialogueBubble;
        [SerializeField] private TextMeshProUGUI dialogueContent;
        [SerializeField] private List<TextMeshProUGUI> choices;
        

        private void Start()
        {
            bubbleStyleSet = DialogueAssetDatabase.instance.bubbleStyleSet;
            
        }

        private IEnumerator InitChoices(List<Choice> currentChoices)
        {
            for (var i = 0; i < currentChoices.Count; i++)
            {
                choices[i].transform.parent.gameObject.SetActive(true);
                choices[i].text = currentChoices[i].text.Split("::")[1];

            }
            for (var i = currentChoices.Count; i < choices.Count; i++)
            {
                choices[i].transform.parent.gameObject.SetActive(false);
            }

            if (currentChoices.Count <= 0) yield break;

            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(choices[0].transform.parent.gameObject);

        }

        public void UpdateUI(DialogueItem dialogueItem)
        {
            
            characterLabel.text = dialogueItem.character;
            dialogueBubble.sprite = bubbleStyleSet.GetSpriteByEmotion(dialogueItem.emotion);
            dialogueContent.text = dialogueItem.line;
            characterSpriteAnim.SetSprite(dialogueItem);
            StartCoroutine(InitChoices(dialogueItem.choices));
            
        
        }

        public void Show()
        {
            bubbleStyleSet ??= DialogueAssetDatabase.instance.bubbleStyleSet;
            dialogueUIRoot.SetActive(true);
            characterSpriteAnim.gameObject.SetActive(true);
        }
        public void Hide(HideStyle style)
        {
            dialogueUIRoot.SetActive(false);
            switch (style)
            {
                case HideStyle.Hide:
                    characterSpriteAnim.gameObject.SetActive(false);
                    return;
                case HideStyle.Fade:
                    characterSpriteAnim.Fade();
                    break;
                default: return;
            }
        }
    }
}