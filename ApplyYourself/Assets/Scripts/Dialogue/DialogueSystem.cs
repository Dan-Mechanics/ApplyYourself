using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class DialogueSystem : StateBehaviour
    {
        public enum ParsingMode { Name, Sprite, Dialogue }
        
        private const char NEWLINE = '~';
        private const char COMMENT = '#';
        private const char QUOTE = '\"';
        
        [SerializeField] private EasyBinding next = default;
        [SerializeField] private EasyBinding exit = default;

        /// <summary>
        /// Consider using a CanvasGroup component.
        /// </summary>
        [SerializeField] private GameObject graphics = default;
        [SerializeField] private TextWriter dialogueWriter = default;
        [SerializeField] private TMP_Text nameText = default;
        [SerializeField] private Image icon = default;
        [SerializeField] private string charactersPath = default;

        private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        private Queue<Frame> pending = new Queue<Frame>();

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (next.WasPressed)
                GoNextFrame();

            if (exit.WasPressed)
                YieldState();
        }

        public void BeginDialogue(TextAsset dialogue)
        {
            pending = ParseDialogue(dialogue);
            GoNextFrame();

            ClaimState();
        }

        private void GoNextFrame()
        {
            if (pending.Count > 0)
            {
                ShowFrame(pending.Dequeue());
            }
            else
            {
                YieldState();
            }
        }

        private Queue<Frame> ParseDialogue(TextAsset dialogue)
        {
            Queue<Frame> result = new Queue<Frame>();
            string text = dialogue.text;
            string[] lines = text.Split(new[] { '\r', '\n' });

            ParsingMode parsingMode = ParsingMode.Name;
            Frame current = default;
            foreach (string line in lines)
            {
                if (!Utils.IsStringValid(line))
                    continue;

                if (line[0] == COMMENT)
                    continue;

                if (line.Length > 2 && line[1] == COMMENT)
                    continue;

                switch (parsingMode)
                {
                    case ParsingMode.Name:
                        current.name = line;
                        parsingMode = ParsingMode.Sprite;
                        break;
                    case ParsingMode.Sprite:
                        current.sprite = line.ToLowerInvariant();
                        parsingMode = ParsingMode.Dialogue;
                        break;
                    case ParsingMode.Dialogue:
                        if (line[0] == QUOTE)
                        {
                            current.text += line.Remove(0, 1);
                        }
                        else if (line[^1] == QUOTE)
                        {
                            current.text += line.Remove(line.Length - 1);
                            current.text = current.text.Replace(NEWLINE, '\n');

                            // NEW ITERATION.
                            result.Enqueue(current);
                            parsingMode = ParsingMode.Name;
                            current = default;
                        }
                        else
                        {
                            current.text += line;
                        }

                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        private void ShowFrame(Frame frame)
        {
            nameText.text = frame.name;
            dialogueWriter.Write(frame.text);

            if (!sprites.ContainsKey(frame.sprite))
                sprites[frame.sprite] = Resources.Load<Sprite>(charactersPath + "/" + frame.sprite);

            icon.sprite = sprites[frame.sprite];
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
            sprites.Clear();
            pending.Clear();
            dialogueWriter.Clear();
        }

        private struct Frame
        {
            public string name;
            public string sprite;
            public string text;
        }
    }
}
