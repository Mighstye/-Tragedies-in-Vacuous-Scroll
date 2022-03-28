using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BossBehaviour;
using Ink.Runtime;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using Utils;
using Utils.Events;

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
        [SerializeField] public string characterID;
        [SerializeField] public DialogueUIRole role;
        [SerializeField] public DialogueUI characterUI;
    }

    public class DialogueSystemManager : Singleton<DialogueSystemManager>
    {
        private const string ActionMap = "Dialogue";
        private static readonly int PhaseFlowSelectedChoice = Animator.StringToHash("selectedChoice");
        [SerializeField] private TextAsset dialogueAsset;
        [SerializeField] private Animator bossPhaseFlow;
        [SerializeField] private List<DialogueUIItem> dialogueUIList;
        [SerializeField] private GameEvent onDialogueContinue;
        [SerializeField] private GameEvent onChoiceConfirm;
        private ControlManager controlManagerRef;
        private DialogueItem currentDialogueItem = new();
        private bool inChoice;
        private bool inDialogue;
        private Story inkStory;
        private LocalizedAsset<TextAsset> localizedAsset;
        public int selectedChoice { get; private set; }

        private void Start()
        {
            controlManagerRef = ControlManager.instance;
            foreach (var ui in dialogueUIList) ui.characterUI.Hide(HideStyle.Hide);
            bossPhaseFlow = BossBehaviourSystemProxy.instance.bossController.phaseFlow;
        }

        public void Init(LocalizedAsset<TextAsset> asset)
        {
            inDialogue = true;
            localizedAsset = asset;
            controlManagerRef.SwitchMap(ActionMap);
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
            bossPhaseFlow.SetInteger(PhaseFlowSelectedChoice, selectedChoice);
            controlManagerRef.SwitchToPlayer();
        }

        public void OnDialogueContinue(InputAction.CallbackContext context)
        {
            if (inChoice || context.phase is not InputActionPhase.Performed) return;
            if (inkStory is null)
            {
                Debug.LogWarning("Ink story is null.");
                return;
            }
            onDialogueContinue.Invoke();
            ContinueDialogue();
        }

        public void MakeChoice(int choiceID)
        {
            selectedChoice = choiceID;
            inkStory.ChooseChoiceIndex(choiceID);
            inChoice = false;
            onChoiceConfirm.Invoke();
            ContinueDialogue();
        }

        private void UpdateUI(bool end = false)
        {
            //Disable all ui panels if dialogue end is reached
            if (end)
            {
                foreach (var ui in dialogueUIList) ui.characterUI.Hide(HideStyle.Hide);
                return;
            }

            var hideStyle = currentDialogueItem.tags.ContainsKey("solo") ? HideStyle.Hide : HideStyle.Fade;
            foreach (var ui in dialogueUIList)
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

        private void UpdateCurrentDialogueLine()
        {
            var rawLine = inkStory.Continue().Split("::");
            currentDialogueItem.character = rawLine[0].Replace(" ", "");
            currentDialogueItem.line = rawLine[1];
            currentDialogueItem.tags = new Dictionary<string, string>();
            foreach (var parsedTag in inkStory.currentTags.Select(inkTag => inkTag.Split(":")))
                currentDialogueItem.tags.Add(parsedTag[0], parsedTag.Length == 2 ? parsedTag[1] : null);
            currentDialogueItem.choices = new List<Choice>();
        }

        private void ContinueDialogue()
        {
            if (inkStory.canContinue)
            {
                UpdateCurrentDialogueLine();
            }
            else if (!inChoice)
            {
                if (inkStory.currentChoices.Count <= 0)
                {
                    inDialogue = false;
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

            UpdateUI(!inDialogue);
            if (!inDialogue) Exit();
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