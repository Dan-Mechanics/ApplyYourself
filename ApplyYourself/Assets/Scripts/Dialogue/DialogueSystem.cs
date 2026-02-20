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
        [SerializeField] private float dialogueCooldown = default;

        private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        private Queue<Frame> pending = new Queue<Frame>();
        private float nextDialogueTime;

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (next.WasPressed && Time.time >= nextDialogueTime)
                GoNextFrame();

            if (exit.WasPressed)
                YieldState();
        }

        public void BeginDialogue(TextAsset dialogue)
        {
            ClaimState();
            Debug.Log(dialogue.text);
            pending = ParseDialogue(dialogue);
            print($"pending count {pending.Count}");
            nextDialogueTime = Time.time + dialogueCooldown;
            GoNextFrame();
        }

        private void GoNextFrame()
        {
            if (pending.Count > 0)
            {
                Debug.Log("yes");
                ShowFrame(pending.Dequeue());
            }
            else
            {
                Debug.Log("is done.");
                YieldState();
            }
        }

        private void ShowFrame(Frame frame)
        {
            nameText.text = frame.name;
            dialogueWriter.Write(frame.text);

            if (!sprites.ContainsKey(frame.sprite))
            {
                string path = charactersPath + "/" + frame.sprite;
                Debug.LogWarning(path);
                Sprite sprite = Resources.Load<Sprite>(path);
                print(sprite != null);

                sprites[frame.sprite] = sprite;
            }


            icon.sprite = sprites[frame.sprite];
            if (icon.sprite == null)
                Debug.LogError($"{frame.sprite}.png does not exist in resources.");
        }

        private Queue<Frame> ParseDialogue(TextAsset dialogue)
        {
            Queue<Frame> result = new Queue<Frame>();
            string[] lines = dialogue.text.Split(new[] { Environment.NewLine.ToCharArray()[0] });
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }

            Debug.Log(lines.Length);
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

                Debug.Log("good" + line);
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
                        string newLine = line;
                        if (newLine[0] == QUOTE)
                            newLine = newLine.Remove(0, 1);

                        if (newLine[^1] != QUOTE)
                        {
                            Debug.LogWarning("else if (line[^1] == QUOTE)");
                            current.text += newLine;
                        }
                        else
                        {
                            current.text += newLine.Remove(newLine.Length - 1);
                            current.text = current.text.Replace(NEWLINE, '\n');
                            Debug.LogWarning("else if (line[^1] == QUOTE)");
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
