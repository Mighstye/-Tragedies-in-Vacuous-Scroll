using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils.Events;

namespace DialogueSystem
{
    public class ChoiceButton : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private int choiceID;
        [SerializeField] private GameEvent onChoiceMove;
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

        public void OnSelect(BaseEventData eventData)
        {
            onChoiceMove.Invoke();
        }
    }
}