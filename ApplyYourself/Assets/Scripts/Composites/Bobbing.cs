using UnityEngine;

namespace ApplyYourself
{
    public class Bobbing : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float lerpSpeed = default;
        [SerializeField, Min(0.1f)] private float minInterval = default;
        [SerializeField, Min(0.1f)] private float maxInterval = default;
        [SerializeField] private Vector3 magnitude = default;
        private Vector3 startingPos;
        private Vector3 targetPos;

        private void Start()
        {
            if (minInterval > maxInterval)
            {
                float temp = minInterval;
                minInterval = maxInterval;
                maxInterval = temp;
            }

            magnitude.x = Mathf.Abs(magnitude.x);
            magnitude.y = Mathf.Abs(magnitude.y);
            magnitude.z = Mathf.Abs(magnitude.z);

            startingPos = transform.localPosition;
            UpdateTargetPosition();
        }
        
        private void FixedUpdate()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, lerpSpeed);
        }

        private void UpdateTargetPosition()
        {
            targetPos = startingPos + new Vector3(
                Random.Range(-magnitude.x, magnitude.x),
                Random.Range(-magnitude.y, magnitude.y),
                Random.Range(-magnitude.z, magnitude.z));

            Invoke(nameof(UpdateTargetPosition), Random.Range(minInterval, maxInterval));
        }
    }
}