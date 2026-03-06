using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class DialogueSystem : StateBehaviour
    {
        public enum ParsingMode { Name, Sprite, Dialogue }
        
        private const char NEWLINE_INDICATOR = '~';
        private const char COMMENT = '#';
        private const char QUOTE = '\"';
        private const char SPACE = ' ';
        
        [SerializeField] private EasyBinding primaryFire = default;
        [SerializeField] private EasyBinding jump = default;
        [SerializeField] private EasyBinding interact = default;

        [SerializeField] private EasyBinding escape = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private TextWriter dialogueWriter = default;
        [SerializeField] private TMP_Text nameText = default;
        [SerializeField] private Image icon = default;
        [SerializeField] private string charactersPath = default;
        [SerializeField] private float dialogueCooldown = default;

        private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        private Queue<Frame> pending = new Queue<Frame>();
        private float nextDialogueTime;
        private InputComposite composite;

        public override void Setup()
        {
            base.Setup();
            composite = new InputComposite(primaryFire, jump, interact);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (escape.WasPressed)
            {
                YieldState();
                return;
            }

            if (composite.WasPressed() && Time.time >= nextDialogueTime)
            {
                if (dialogueWriter.IsDone)
                {
                    GoNextFrame();
                }
                else
                {
                    dialogueWriter.ForceComplete();
                }
            }
        }

        public void BeginDialogue(TextAsset dialogue)
        {
            ClaimState();
            pending = ParseDialogue(dialogue.text);
            GoNextFrame();
        }

        private void GoNextFrame()
        {
            nextDialogueTime = Time.time + dialogueCooldown;
            if (pending.Count > 0)
            {
                ShowFrame(pending.Dequeue());
            }
            else
            {
                YieldState();
            }
        }

        private void ShowFrame(Frame frame)
        {
            nameText.text = frame.name;
            dialogueWriter.Write(frame.dialogue);

            if (!sprites.ContainsKey(frame.sprite))
                sprites[frame.sprite] = Resources.Load<Sprite>(charactersPath + "/" + frame.sprite);

            icon.sprite = sprites[frame.sprite];
            if (icon.sprite == null)
                Debug.LogError($"{frame.sprite}.png does not exist in resources.");
        }

        private Queue<Frame> ParseDialogue(string dialogue)
        {
            Queue<Frame> result = new Queue<Frame>();
            string[] lines = dialogue.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            Frame current = default;
            ParsingMode parsingMode = ParsingMode.Name;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (!Utils.IsStringValid(line))
                    continue;

                if (line[0] == COMMENT)
                    continue;

                if (line.Length >= 2 && line[1] == COMMENT)
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
                            line = line.Remove(0, 1);

                        if (line[^1] != QUOTE)
                        {
                            current.dialogue += line;
                            if (line[^1] != NEWLINE_INDICATOR)
                                current.dialogue += SPACE;
                        }
                        else
                        {
                            current.dialogue += line.Remove(line.Length - 1);
                            current.dialogue = current.dialogue.Replace(NEWLINE_INDICATOR, '\n');

                            // NEW ITERATION.
                            result.Enqueue(current);
                            parsingMode = ParsingMode.Name;
                            current = default;
                        }

                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public override void Enter()
        {
            base.Enter();
            canvasGroup.alpha = 1f;
        }

        public override void Exit()
        {
            base.Exit();
            canvasGroup.alpha = 0f;
            dialogueWriter.Clear();
            sprites.Clear();
            pending.Clear();
        }

        private struct Frame
        {
            public string name;
            public string sprite;
            public string dialogue;
        }
    }
}
