using UnityEngine;

namespace ApplyYourself
{
    public class ThirdPersonLook : StateBehaviour
    {

        [SerializeField] private Transform leftRightPivot = default;
        [SerializeField] private Transform upDownPivot = default;
        [SerializeField] private Transform eyes = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private Vector2 rotation = default;
        [SerializeField] private float sensitivity = default;
        [SerializeField] private float maxAngle = default;
        [SerializeField] private float distance = default;
        [SerializeField, Min(0f)] private float offset = default;

        public override void OnUpdate()
        {
            base.OnUpdate();
            UpdatePivot();
            UpdateEyesDistance();
        }

        private void UpdatePivot()
        {
            Vector2 look = new Vector2(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
            rotation += look * sensitivity;
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
