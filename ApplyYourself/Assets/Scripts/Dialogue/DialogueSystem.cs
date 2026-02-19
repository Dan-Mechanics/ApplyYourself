using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// todo: 
    /// implement friendly parsing for the script,
    /// implement textwriter slow typer and use ~ as newline symbol
    /// implement some state for everythign type beat
    /// </summary>
    public class DialogueSystem : StateBehaviour
    {
        private const char NEWLINE = '~';
        private const char COMMENT = '#';
        
        [SerializeField] private EasyBinding skip = default;
        [SerializeField] private EasyBinding exit = default;
        [SerializeField] private GameObject graphics = default;
        [SerializeField] private TMP_Text dialogueText = default;
        [SerializeField] private string characterSpriteDirectory = default;

        private readonly Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();
        private Queue<Frame> frames = new Queue<Frame>();

        public override void OnUpdate()
        {
            base.OnUpdate();
            // chekc for skip key.
            if (skip.WasPressed)
            {

            }

            if (exit.WasPressed)
                YieldState();
        }

        public void ShowDialogue(TextAsset dialogue)
        {
            ClaimState();

            // parse the dialogue into actual steps.
            // make a queue or something idk.
        }

        public override void Enter()
        {
            base.Enter();
            graphics.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            graphics.SetActive(false);
            characterSprites.Clear();
            frames.Clear();

            dialogueText.text = string.Empty;
        }

        private struct Frame
        {
            public string characterName;
            public string spriteName;
            public string dialogue;
        }
    }
}
