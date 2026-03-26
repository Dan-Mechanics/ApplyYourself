using System;
using System.Text;
using UnityEngine;

namespace ApplyYourself
{
    [Serializable]
    public class GoToQuest : IQuest
    {
       // public string playerTag;
       // public string targetTag;
        public float minDistance;
        
        public event Action<IQuest> OnQuestFinished;
        public event Action<string> OnDisplayString;

        private readonly StringBuilder builder = new StringBuilder();
        private readonly Transform player;
        private readonly Transform point;

        public GoToQuest(Transform player, Transform point)
        {
            this.player = player;
            this.point = point;
        }

        public void Setup() => ShowFeedback(0f);

        public void FixedUpdate()
        {
            float dist = Vector3.Distance(player.position, point.position);
            ShowFeedback(dist);
            if (dist < minDistance)
                OnQuestFinished?.Invoke(this);
        }

        private void ShowFeedback(float dist)
        {
            builder.AppendLine($"Go to {point.name} ( {dist}m )");
            OnDisplayString?.Invoke(builder.ToString());
            builder.Clear();
        }
    }
}
