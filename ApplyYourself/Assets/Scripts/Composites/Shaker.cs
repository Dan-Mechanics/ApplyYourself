using UnityEngine;

namespace ApplyYourself
{
    public class Shaker : MonoBehaviour
    {
        [SerializeField] private float maxAngle = default;
        [SerializeField] private float speed = default;
        private float direction;
        private float angle;

        private void Start() => direction = -1f;

        private void FixedUpdate()
        {
            if (angle > maxAngle)
            {
                direction = -1f;
            }
            else if (angle < -maxAngle)
            {
                direction = 1f;
            }

            angle += speed * direction * Time.fixedDeltaTime;
            transform.localEulerAngles = Vector3.forward * angle;
        }
    }
}
