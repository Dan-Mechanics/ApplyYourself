using UnityEngine;

namespace ApplyYourself
{
    public class ThirdPersonLook : StateBehaviour
    {

        [SerializeField] private Transform leftRightPivot = default;
        [SerializeField] private Transform upDownPivot = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private Vector2 rotation = default;
        [SerializeField] private float sensitivity = default;
        [SerializeField] private float maxAngle = default;
        [SerializeField] private float distance = default;
        [SerializeField] private float minDistance = default;
        [SerializeField] private float maxDistance = default;
        [SerializeField] private float zoomSensitivity = default;
        [SerializeField, Min(0f)] private float offset = default;
        private ILookInput lookInput;
        private Transform eyes;

        public void Setup(ILookInput lookInput, Transform eyes)
        {
            this.lookInput = lookInput;
            this.eyes = eyes;
            eyes.SetParent(upDownPivot);
            eyes.localPosition = Vector3.zero;
            eyes.localRotation = Quaternion.identity;
            eyes.localScale = Vector3.one;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            UpdatePivot();
            UpdateEyesDistance();
            Scroll();
        }

        private void Scroll()
        {
            distance -= lookInput.GetScroll() * zoomSensitivity;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        private void UpdatePivot()
        {
            rotation += lookInput.GetLook() * sensitivity;
            rotation.x = Mathf.Clamp(rotation.x, -maxAngle, maxAngle);

            upDownPivot.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.right);
            leftRightPivot.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.up);
        }

        private void UpdateEyesDistance()
        {
            float dist = distance;
            if (Physics.Raycast(eyes.parent.position, -eyes.forward, out RaycastHit hit, distance + offset, mask, QueryTriggerInteraction.Ignore))
                dist = hit.distance - offset;

            dist = Mathf.Clamp(dist, 0f, distance);
            eyes.localPosition = Vector3.back * dist;
        }
    }
}
