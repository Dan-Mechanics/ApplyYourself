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
        private DateTime doneTime;

        public void Begin()
        {
            doneTime = DateTime.Now;
            doneTime = doneTime.AddSeconds(doneSeconds);
            doneTime = doneTime.AddMinutes(doneMinutes);
            InvokeRepeating(nameof(Tick), invokeInterval, invokeInterval);
        }

        public void End() => CancelInvoke(nameof(Tick));

        private void Tick()
        {
            TimeSpan timeSpan = doneTime - DateTime.Now;
            int mins = timeSpan.Minutes;
            int secs = timeSpan.Seconds;

            OnNewTime?.Invoke(mins, secs);
            if (mins > 0 || secs > 0)
                return;

            OnDone?.Invoke();
            End();
        }
    }
}
