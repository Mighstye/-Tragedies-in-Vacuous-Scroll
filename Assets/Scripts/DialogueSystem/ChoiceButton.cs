using System;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class ChoiceButton : MonoBehaviour
    {
        private Button button;

        private DialogueSystemManager dialogueSystemManagerRef;
        [SerializeField] private int choiceID;
        private void Start()
        {
            dialogueSystemManagerRef = DialogueSystemManager.instance;
            button = GetComponent<Button>();
            button.onClick.AddListener(SendChoiceID);
        }

        private void SendChoiceID()
        {
            dialogueSystemManagerRef.MakeChoice(choiceID);
        }
    }
}