using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class DialogueSystem : StateBehaviour
    {
        public enum ParsingMode { Name, Sprite, Dialogue }
        public event Action<TextAsset> OnDialogue;

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

        public void Setup()
            => composite = new InputComposite(primaryFire, jump, interact);

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
                    dialogueWriter.Skip();
                }
            }
        }

        public void BeginDialogue(TextAsset dialogue)
        {
            if (Time.time < nextDialogueTime)
                return;

            OnDialogue?.Invoke(dialogue);

            ClaimState();
            pending = Parse(dialogue.text);
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
            nameText.text = frame.characerName;
            dialogueWriter.Write(frame.dialogue);

            if (!sprites.ContainsKey(frame.spriteName))
                sprites[frame.spriteName] = Resources.Load<Sprite>(charactersPath + "/" + frame.spriteName);

            Sprite sprite = sprites[frame.spriteName];
            if (sprite == null)
            {
                Debug.LogError($"{frame.spriteName}.png does not exist in resources.");
                return;
            }

            icon.sprite = sprite;
            icon.SetNativeSize();
        }

        private Queue<Frame> Parse(string dialogue)
        {
            Queue<Frame> frames = new Queue<Frame>();
            if (!Utils.IsStringValid(dialogue))
                return frames;

            Frame current = default;
            ParsingMode parsingMode = ParsingMode.Name;
            foreach (string line in SplitToLines(dialogue))
            {
                try
                {
                    ParseLine(line, frames, ref current, ref parsingMode);
                }
                catch (Exception exception)
                {
                    Debug.LogWarning(exception.Message);
                }
            }

            return frames;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/1547476/split-a-string-on-newlines-in-net
        /// </summary>
        private IEnumerable<string> SplitToLines(string str)
        {
            if (!Utils.IsStringValid(str))
                yield break;

            using System.IO.StringReader reader = new System.IO.StringReader(str);
            string line;
            while ((line = reader.ReadLine()) != null)
                yield return line;
        }

        private void ParseLine(string line, Queue<Frame> frames, ref Frame current, ref ParsingMode parsingMode)
        {
            line = line.Trim();
            if (!Utils.IsStringValid(line))
                return;

            if (line[0] == COMMENT)
                return;

            if (line.Length >= 2 && line[1] == COMMENT)
                return;

            switch (parsingMode)
            {
                case ParsingMode.Name:
                    current.characerName = line;
                    parsingMode = ParsingMode.Sprite;
                    break;
                case ParsingMode.Sprite:
                    current.spriteName = line.ToLowerInvariant();
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
                        frames.Enqueue(current);
                        parsingMode = ParsingMode.Name;
                        current = default;
                    }

                    break;
                default:
                    break;
            }
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
            public string characerName;
            public string spriteName;
            public string dialogue;
        }
    }
}
