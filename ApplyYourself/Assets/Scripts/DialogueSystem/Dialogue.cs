using UnityEngine;

namespace ApplyYourself
{
    public class Dialogue : MonoBehaviour, IInteractable
    {
        [SerializeField] private TextAsset dialogue = default;
        private DialogueSystem dialogueSystem;
        private string highlight;

        private void Start() => SetDialogue(dialogue);
        public string GetHighlight() => highlight;
        public Vector3 GetPosition() => transform.position;

        public void Interact()
        {
            if (!dialogueSystem)
                dialogueSystem = FindAnyObjectByType<DialogueSystem>();

            dialogueSystem.BeginDialogue(dialogue);
        }

        public void SetDialogue(TextAsset dialogue)
        {
            this.dialogue = dialogue;
            highlight = dialogue.name;
            if (highlight.Contains('_'))
                highlight = highlight.Split('_')[0];

            highlight = $"Talk to {highlight}";
        }
    }
}