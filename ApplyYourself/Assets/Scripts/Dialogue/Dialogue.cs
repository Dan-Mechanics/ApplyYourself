using System;
using UnityEngine;

namespace ApplyYourself
{
    public class Dialogue : MonoBehaviour, IInteractable
    {
        [SerializeField] private TextAsset dialogue = default;

        public void Interact()
        {
            DialogueSystem dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            if (dialogueSystem == null)
                return;

            dialogueSystem.ShowDialogue(dialogue);
        }

        public void SetDialogue(TextAsset dialogue) => this.dialogue = dialogue;
    }
}
