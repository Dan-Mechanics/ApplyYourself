using System;
using System.Text;
using UnityEngine;

namespace ApplyYourself
{
    [Serializable]
    public class GoToQuest : IQuest
    {
        public string playerTag;
        public string targetTag;
        public float minDistance;
        
        public event Action<IQuest> OnDone;
        public event Action<string> OnFeedback;

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
            Debug.Log("og" + dist.ToString());
            if (dist < minDistance)
                OnDone?.Invoke(this);
        }

        private void ShowFeedback(float dist)
        {
            builder.AppendLine(IQuest.LINE);
            Debug.Log(dist);
            builder.AppendLine($"Go to {point.name} ( {dist}m left ... )");
            builder.Append(IQuest.LINE);
            OnFeedback?.Invoke(builder.ToString());
            builder.Clear();
        }
    }
}
