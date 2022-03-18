using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
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
        private LocalizedString localizedCharacterName = new LocalizedString();

        private void Start()
        {
            bubbleStyleSet = DialogueAssetDatabase.instance.bubbleStyleSet;
            
        }

        private IEnumerator InitChoices(IReadOnlyList<Choice> currentChoices)
        {
            //Enable used choice UI and update choice text
            for (var i = 0; i < currentChoices.Count; i++)
            {
                choices[i].transform.parent.gameObject.SetActive(true);
                choices[i].text = currentChoices[i].text.Split("::")[1];

            }
            //Disable unused choice UI
            for (var i = currentChoices.Count; i < choices.Count; i++)
            {
                choices[i].transform.parent.gameObject.SetActive(false);
            }

            if (currentChoices.Count <= 0) yield break;
            //Sets the default selected choice
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(choices[0].transform.parent.gameObject);
        }

        public void UpdateUI(DialogueItem dialogueItem)
        {
            if (dialogueItem.tags.ContainsKey("hideName"))
            {
                characterLabel.text = "??????";
            }else StartCoroutine(LocalizeCharacterName(dialogueItem.character));
            dialogueContent.text = dialogueItem.line;
            
            var emotion = dialogueItem.tags.ContainsKey("emotion") ? dialogueItem.tags["emotion"] : null;
            dialogueBubble.sprite = bubbleStyleSet.GetSpriteByEmotion(emotion);
            characterSpriteAnim.SetSprite(dialogueItem.character,emotion);
            StartCoroutine(InitChoices(dialogueItem.choices));
        }
        
        private IEnumerator LocalizeCharacterName(string key)
        {
            localizedCharacterName.TableReference = "Character Names";
            localizedCharacterName.TableEntryReference = key;
            var localizedString = localizedCharacterName.GetLocalizedStringAsync();
            yield return localizedString;
            if(localizedString.IsDone)characterLabel.text=localizedString.Result;
        }

        public void Show()
        {
            bubbleStyleSet ??= DialogueAssetDatabase.instance.bubbleStyleSet;
            transform.SetSiblingIndex(transform.parent.childCount-1); //Put this item on top of other dialogue UI
            dialogueUIRoot.SetActive(true);
            characterSpriteAnim.gameObject.SetActive(true);
            characterSpriteAnim.Restore();
        }
        public void Hide(HideStyle style)
        {
            dialogueUIRoot.SetActive(false);
            switch (style)
            {
                case HideStyle.Hide:
                    characterSpriteAnim.gameObject.SetActive(false);
                    break;
                case HideStyle.Fade:
                    characterSpriteAnim.Fade();
                    break;
                default: return;
            }
        }
    }
}