using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ApplyYourself
{
    public class QuestHandler : StateBehaviour
    {
        [SerializeField] private TMP_Text text = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        private readonly Queue<IQuest> quests = new Queue<IQuest>();
        private IQuest current;

        public void AddQuest(IQuest quest) => quests.Enqueue(quest);

        public void BeginQuest()
        {
            if (quests.Count <= 0)
                return;

            Display(string.Empty);
            current = quests.Dequeue();
            current.OnDone += EndQuest;
            current.OnFeedback += Display;
            current.Setup();
        }

        private void EndQuest(IQuest quest) 
        {
            quest.OnDone -= EndQuest;
            quest.OnFeedback -= Display;
            BeginQuest();
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
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            current?.FixedUpdate();
        }

        private void Display(string str) => text.text = str;
    }
}
