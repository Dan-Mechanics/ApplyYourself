using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ApplyYourself
{
    public class QuestHandler : StateBehaviour
    {
        [SerializeField] private TMP_Text text = default;
        private readonly Queue<IQuest> quests = new Queue<IQuest>();
        private IQuest current;

        public void AddQuest(IQuest quest) => quests.Enqueue(quest);

        public void Assign()
        {
            Display(string.Empty);
            BeginQuest(quests.Dequeue());
        }

        private void BeginQuest(IQuest quest)
        {
            current = quest;
            quest.OnDone += EndQuest;
            quest.OnFeedback += Display;
            quest.Setup();
        }

        private void EndQuest(IQuest quest) 
        {
            quest.OnDone -= EndQuest;
            quest.OnFeedback -= Display;
            if (quests.Count > 0)
                BeginQuest(quests.Dequeue());
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            current?.FixedUpdate();
        }

        private void Display(string str) => text.text = str;
    }
}
