using System;
using UnityEngine;

namespace ApplyYourself
{
    public class Dialogue : MonoBehaviour, IInteractable
    {
        public event Action<TextAsset> OnDialogue;
        [SerializeField] private TextAsset dialogue = default;

        public void Interact()
        {
            // throw evneti n dialgoue system type beat.
            print(dialogue.text);
            OnDialogue?.Invoke(dialogue);
        }

        public void SetDialogue(TextAsset dialogue) => this.dialogue = dialogue;
    }
}
