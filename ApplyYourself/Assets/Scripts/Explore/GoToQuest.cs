using System;
using System.Text;
using UnityEngine;

namespace ApplyYourself
{
    public class GoToQuest : IQuest
    {
        public event Action<IQuest> OnDone;
        public event Action<string> OnFeedback;

        private readonly StringBuilder builder = new StringBuilder();
        private readonly Transform player;
        private readonly Transform point;
        private readonly float minDistance;

        public GoToQuest(Transform player, Transform point, float minDistance)
        {
            this.player = player;
            this.point = point;
            this.minDistance = minDistance;
        }

        public void Setup() => ShowFeedback(0f);

        public void FixedUpdate()
        {
            float dist = Vector3.Distance(point.position, player.position);
            ShowFeedback(dist);
            if (dist < minDistance)
                OnDone?.Invoke(this);
        }

        private void ShowFeedback(float dist)
        {
            builder.AppendLine(IQuest.LINE);
            builder.AppendLine($"Go to {point.name} ( {dist}m left ... )");
            builder.Append(IQuest.LINE);
            OnFeedback?.Invoke(builder.ToString());
            builder.Clear();
        }
    }
}
