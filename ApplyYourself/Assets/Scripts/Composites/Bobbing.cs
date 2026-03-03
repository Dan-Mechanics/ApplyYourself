using UnityEngine;

namespace ApplyYourself
{
    public class Bobbing : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float lerpSpeed = default;
        [SerializeField, Min(0f)] private float minInterval = default;
        [SerializeField, Min(0f)] private float maxInterval = default;
        [SerializeField] private Vector3 startingPos = default;
        [SerializeField] private Vector3 magnitude = default;
        private Vector3 targetPos;

        private void Start()
        {
            SetPosition(startingPos);
            SetTargetPos();
        }
        
        private void FixedUpdate() => SetPosition(Vector3.Lerp(transform.localPosition, targetPos, lerpSpeed));

        private void SetTargetPos()
        {
            targetPos = startingPos + new Vector3(
                Random.Range(-magnitude.x, magnitude.x),
                Random.Range(-magnitude.y, magnitude.y),
                Random.Range(-magnitude.z, magnitude.z));

            Invoke(nameof(SetTargetPos), Random.Range(minInterval, maxInterval));
        }
        
        private void OnValidate()
        {
            startingPos = transform.localPosition;
        }

        private void SetPosition(Vector3 pos) => transform.localPosition = pos;
    }
}