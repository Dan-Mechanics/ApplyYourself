using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    ///  future: base behaviour here??
    /// </summary>
    public class LerpFollow : MonoBehaviour
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private float lerpSpeed = default;
        [SerializeField] private bool lookAt = default;

        private void FixedUpdate()
        {
            transform.SetPositionAndRotation(
                Vector3.Lerp(transform.position, target.position, lerpSpeed),
                Quaternion.Lerp(transform.rotation, target.rotation, lerpSpeed));

            if (lookAt)
                transform.LookAt(target.parent);
        }
    }
}
