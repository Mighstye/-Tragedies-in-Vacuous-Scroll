using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class ChoiceButton : MonoBehaviour
    {
        [SerializeField] private int choiceID;
        private Button button;

        private DialogueSystemManager dialogueSystemManagerRef;

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