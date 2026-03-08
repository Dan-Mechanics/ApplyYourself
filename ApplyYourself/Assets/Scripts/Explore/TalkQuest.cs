using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ApplyYourself
{
    public class TalkQuest : IQuest
    {
        public event Action<IQuest> OnDone;
        public event Action<string> OnFeedback;

        private readonly StringBuilder builder = new StringBuilder();
        private readonly HashSet<TextAsset> readDialogue = new HashSet<TextAsset>();
        private readonly int doneCount;

        public TalkQuest(int doneCount)
        {
            this.doneCount = doneCount;
        }

        public void Setup() => ShowFeedback();

        public void OnDialogue(TextAsset dialogue)
        {
            readDialogue.Add(dialogue);
            ShowFeedback();
            if (readDialogue.Count >= doneCount)
                OnDone?.Invoke(this);
        }

        private void ShowFeedback()
        {
            builder.AppendLine(IQuest.LINE);
            builder.AppendLine($"Talk to ( {readDialogue.Count} / {doneCount} ) ...");
            builder.Append(IQuest.LINE);
            OnFeedback?.Invoke(builder.ToString());
            builder.Clear();
        }

    }
}
