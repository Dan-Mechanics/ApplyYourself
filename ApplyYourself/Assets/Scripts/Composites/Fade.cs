using System;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Must account for both fade in and out.
    /// </summary>
    public class Fade : StateBehaviour
    {
        public event Action OnFadeComplete;

        [SerializeField] private CanvasGroup group = default;
        [SerializeField, Min(0.01f)] private float fadeTime = default;
        private bool fadeIn;
        private float direction;
        private float alphaTarget;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (fadeIn)
                return;

            ProgressFade();
            if (group.alpha >= alphaTarget)
            {
                OnFadeComplete?.Invoke();
                YieldState();
            }
        }

        private void FixedUpdate()
        {
            if (!fadeIn)
                return;

            ProgressFade();
            if (group.alpha <= alphaTarget)
            {
                OnFadeComplete?.Invoke();
                enabled = false;
            }
        }

        private void ProgressFade()
        {
            float alpha = group.alpha;
            alpha += direction * (1f / fadeTime) * Time.fixedDeltaTime;
            alpha = Mathf.Clamp01(alpha);
            group.alpha = alpha;
        }

        public void BeginFade(bool fadeIn) 
        {
            this.fadeIn = fadeIn;
            group.alpha = fadeIn ? 1f : 0f;
            direction = fadeIn ? -1f : 1f;
            alphaTarget = fadeIn ? 0f : 1f;
            enabled = true;

            if (!fadeIn)
                ClaimState();
        }

        private void OnValidate()
        {
            if (group == null)
                group = GetComponent<CanvasGroup>();
        }
    }
}