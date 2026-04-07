using UnityEngine;

namespace ApplyYourself
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator = default;
        [SerializeField, Min(0f)] private float crossfadeTime = default;

        public void Crossfade(string animationName)
        {
            animator.CrossFadeInFixedTime(animationName, crossfadeTime);
        }
    }
}
