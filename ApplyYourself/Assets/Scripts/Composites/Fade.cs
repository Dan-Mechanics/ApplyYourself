using UnityEngine;

namespace ApplyYourself
{
    public class Fade : StateBehaviour
    {
        [SerializeField] private CanvasGroup group = default;
        [SerializeField, Min(0.01f)] private float fadeTime = default;
        private float direction;
        private float endPoint;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            float alpha = group.alpha;
            alpha += direction * (1f / fadeTime) * Time.fixedDeltaTime;
            alpha = Mathf.Clamp01(alpha);
            group.alpha = alpha;

            if (alpha == endPoint)
                YieldState();
        }

        public void BeginFade(bool fadeIn) 
        {
            group.alpha = fadeIn ? 1f : 0f;
            direction = fadeIn ? -1f : 1f;
            endPoint = fadeIn ? 0f : 1f;
            print($"begin fade {fadeIn}");
            ClaimState();
        }

        private void OnValidate() => group = GetComponent<CanvasGroup>();
    }
}