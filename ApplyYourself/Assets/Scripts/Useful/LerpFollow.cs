using UnityEngine;

namespace ApplyYourself
{
    public class LerpFollow : MonoBehaviour
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private Transform lookAt = default;
        [SerializeField] private float lerpSpeed = default;

        private void FixedUpdate()
        {
            if (target != null)
                transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed);

            if (lookAt != null)
                transform.LookAt(lookAt);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
            transform.SetPositionAndRotation(target.position, target.rotation);
        }
    }
}
