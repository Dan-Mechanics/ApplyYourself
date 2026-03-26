using UnityEngine;

namespace ApplyYourself
{
    public class Fade : StateBehaviour
    {
        [SerializeField] private CanvasGroup group = default;
        [SerializeField, Min(0.01f)] private float fadeTime = default;
        private bool fadeIn;
        private float direction;
        private float endPoint;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (!fadeIn && Move() == endPoint)
                YieldState();
        }

        private void FixedUpdate()
        {
            if (fadeIn && Move() == endPoint)
                Destroy(this);
        }

        private float Move()
        {
            float alpha = group.alpha;
            alpha += direction * (1f / fadeTime) * Time.fixedDeltaTime;
            alpha = Mathf.Clamp01(alpha);
            group.alpha = alpha;
            return alpha;
        }

        public void BeginFade(bool fadeIn) 
        {
            this.fadeIn = fadeIn;
            group.alpha = fadeIn ? 1f : 0f;
            direction = fadeIn ? -1f : 1f;
            endPoint = fadeIn ? 0f : 1f;

            if (!fadeIn)
                ClaimState();
        }

        private void OnValidate() => group = GetComponent<CanvasGroup>();
    }
}