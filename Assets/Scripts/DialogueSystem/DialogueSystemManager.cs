using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using BossBehaviour;
using Ink.Runtime;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using Utils;

namespace DialogueSystem
{
    public enum DialogueUIRole
    {
        Protagonist,
        Antagonist
    }
    [Serializable]
    public class DialogueUIItem
    {
        [SerializeField]public string characterID;
        [SerializeField] public DialogueUIRole role;
        [SerializeField]public DialogueUI characterUI;
    }
    public class DialogueSystemManager : Singleton<DialogueSystemManager>
    {
        private ControlManager controlManagerRef;
        [SerializeField] private TextAsset dialogueAsset;
        [SerializeField] private Animator bossPhaseFlow;
        [SerializeField] private List<DialogueUIItem> dialogueUIList;
        private LocalizedAsset<TextAsset> localizedAsset;
        private Story inkStory;
        private DialogueItem currentDialogueItem = new DialogueItem();
        private bool inDialogue = false;
        private bool inChoice = false;
        private static readonly int PhaseFlowSelectedChoice = Animator.StringToHash("selectedChoice");
        public int selectedChoice { get; private set; }

        private void Start()
        {
            controlManagerRef = ControlManager.instance;
            foreach (var ui in dialogueUIList)
            {
                ui.characterUI.Hide(HideStyle.Hide);
            }
        }

        public void Init(LocalizedAsset<TextAsset> asset)
        {
            inDialogue = true;
            localizedAsset = asset;
            controlManagerRef.SwitchToDialogue();
            StartCoroutine(InitLocalizedStory());
        }
        
        private IEnumerator InitLocalizedStory()
        {
            var localized = localizedAsset.LoadAssetAsync();
            yield return localized;
            if (!localized.IsDone) yield break;
            dialogueAsset = localized.Result;
            inkStory = new Story(dialogueAsset.text);
            ContinueDialogue();
        }

        private void Exit()
        {
            inDialogue = false;
            inChoice = false;
            bossPhaseFlow.SetInteger(PhaseFlowSelectedChoice,selectedChoice);
            controlManagerRef.SwitchToPlayer();
        }

        public void OnDialogueContinue(InputAction.CallbackContext context)
        {
            if (inChoice||context.phase is not InputActionPhase.Performed) return;
            if (inkStory is null)
            {
                Debug.LogWarning("Ink story is null.");
                return;
            }
            ContinueDialogue();
        }

        public void MakeChoice(int choiceID)
        {
            selectedChoice = choiceID;
            inkStory.ChooseChoiceIndex(choiceID);
            inChoice = false;
            ContinueDialogue();
        }

        private void UpdateUI(bool end = false)
        {
            //Disable all ui panels if dialogue end is reached
            if (end)
            {
                foreach (var ui in dialogueUIList)
                {
                    ui.characterUI.Hide(HideStyle.Hide);
                }
                return;
            }

            var hideStyle = currentDialogueItem.tags.ContainsKey("solo") ? HideStyle.Hide : HideStyle.Fade;
            foreach (var ui in dialogueUIList)
            {
                if (ui.characterID == currentDialogueItem.character)
                {
                    ui.characterUI.Show();
                    ui.characterUI.UpdateUI(currentDialogueItem);
                }
                else
                {
                    ui.characterUI.Hide(hideStyle);
                }
            }
        }

        private void UpdateCurrentDialogueLine()
        {
            var rawLine = inkStory.Continue().Split("::");
            currentDialogueItem.character = rawLine[0].Replace(" ",""); 
            currentDialogueItem.line = rawLine[1]; 
            currentDialogueItem.tags = new Dictionary<string, string>();
            foreach (var parsedTag in inkStory.currentTags.Select(inkTag => inkTag.Split(":")))
            {
                currentDialogueItem.tags.Add(parsedTag[0], parsedTag.Length == 2 ? parsedTag[1] : null);
            }
            currentDialogueItem.choices = new List<Choice>();
        }

        private void ContinueDialogue() {
            if (inkStory.canContinue)
            {
                UpdateCurrentDialogueLine();
            }
            else if (!inChoice)
            {
                if (inkStory.currentChoices.Count <= 0)
                {
                    inDialogue=false;
                }
                else
                {
                    inChoice = true;
                    currentDialogueItem.choices = inkStory.currentChoices;
                }
            }
            else
            {
                currentDialogueItem = null;
                inDialogue = false;
            }
            UpdateUI(end:!inDialogue);
            if (!inDialogue)
            {
                Exit();
            }
        }

        public void AssignUI(BossAsset bossAsset)
        {
            foreach (var dialogueUIItem in dialogueUIList.Where(dialogueUIItem =>
                         dialogueUIItem.role is DialogueUIRole.Antagonist))
            {
                dialogueUIItem.characterID = bossAsset.characterID;
                return;
            }
        }
        
        
    }
}