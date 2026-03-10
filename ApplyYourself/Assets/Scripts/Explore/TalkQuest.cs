using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ApplyYourself
{
   [Serializable]
    public class TalkQuest : IQuest
    {
        public int doneCount;
        
        public event Action<IQuest> OnDone;
        public event Action<string> OnFeedback;

        private readonly StringBuilder builder = new StringBuilder();
        private readonly HashSet<TextAsset> seenDialogue = new HashSet<TextAsset>();

        public void Setup() => ShowFeedback();

        public void OnDialogue(TextAsset dialogue)
        {
            seenDialogue.Add(dialogue);
            ShowFeedback();
            if (seenDialogue.Count >= doneCount)
                OnDone?.Invoke(this);
        }

        private void ShowFeedback()
        {
            builder.AppendLine(IQuest.LINE);
            builder.AppendLine($"Talk to ( {seenDialogue.Count} / {doneCount} ) ...");
            builder.Append(IQuest.LINE);
            OnFeedback?.Invoke(builder.ToString());
            builder.Clear();
        }

    }
}
