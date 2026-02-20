using System;
using UnityEngine;

namespace ApplyYourself
{
    public class Timer : MonoBehaviour
    {
        public event Action<int, int> OnNewTime;
        public event Action OnDone;

        [SerializeField] private float doneMinutes = default;
        [SerializeField] private float doneSeconds = default;
        [SerializeField] private float invokeInterval = default;
        private DateTime startingPoint;

        public void Begin()
        {
            startingPoint = DateTime.Now;
            InvokeRepeating(nameof(Tick), 0f, invokeInterval);
        }

        public void End() => CancelInvoke(nameof(Tick));

        private void Tick()
        {
            TimeSpan timeSpan = DateTime.Now - startingPoint;
            int min = timeSpan.Minutes;
            int sec = timeSpan.Seconds;

            OnNewTime?.Invoke(min, sec);
            if (min < doneMinutes || sec < doneSeconds)
                return;

            OnDone?.Invoke();
            End();
        }
    }
}
