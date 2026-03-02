using UnityEngine;

namespace ApplyYourself
{
    public class AdaptiveSunRotation : MonoBehaviour
    {
        [SerializeField] private Transform sun = default;

        private void FixedUpdate()
        {
            sun.forward = transform.forward;
            sun.Rotate(Vector3.up * 180, Space.World);
        }
    }
}
