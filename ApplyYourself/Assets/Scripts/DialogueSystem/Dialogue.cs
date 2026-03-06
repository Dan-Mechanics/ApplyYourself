using UnityEngine;

namespace ApplyYourself
{
    public class Dialogue : MonoBehaviour, IInteractable
    {
        [SerializeField] private TextAsset dialogue = default;
        private DialogueSystem dialogueSystem;

        public string GetHighlight() => $"Talk to {dialogue.name}";
        public Vector3 GetPosition() => transform.position;

        public void Interact()
        {
            if (dialogueSystem == null)
                dialogueSystem = FindAnyObjectByType<DialogueSystem>();

            if (dialogueSystem != null)
                dialogueSystem.BeginDialogue(dialogue);
        }

        public void SetDialogue(TextAsset dialogue) => this.dialogue = dialogue;
    }
}
