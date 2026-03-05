using UnityEngine;

namespace ApplyYourself
{
    public class PivotController : StateBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform target = default;
        [SerializeField] private EasyBinding primaryFire = default;
        [SerializeField] private EasyBinding rotate = default;
        [SerializeField] private EasyBinding move = default;

        [Header("Rotation")]
        [SerializeField] private Vector3 rotation = default;
        [SerializeField] private float sensitivity = default;
        [SerializeField] private float minAngle = default;
        [SerializeField] private float maxAngle = default;

        [Header("Zoom")]
        [SerializeField] private Vector3 position = default;
        [SerializeField] private float zoomSensitivity = default;
        [SerializeField] private float minDistance = default;
        [SerializeField] private float maxDistance = default;

        [Header("Position")]
        [SerializeField] private float moveSensitivity = default;

        public override void Exit()
        {
            base.Exit();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (primaryFire.WasPressed || move.WasReleased || rotate.WasReleased)
            {
                YieldState();
                return;
            }
            
            if (move.IsHeld && moveSensitivity > 0f)
            {
                Move();
            }
            else if (rotate.IsHeld)
            {
                Rotate();
            }
        }

        private void Update()
        {
            Scroll();
            Visualize();
        }

        private void Scroll()
        {
            position.z += Input.mouseScrollDelta.y * zoomSensitivity;
            position.z = Mathf.Clamp(position.z, -maxDistance, -minDistance);
        }

        private void Rotate()
        {
            rotation.y += sensitivity * Input.GetAxisRaw("Mouse X");
            rotation.x -= sensitivity * Input.GetAxisRaw("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, minAngle, maxAngle);
        }

        private void Move()
        {
            float y = -Input.GetAxisRaw("Mouse Y");
            float x = -Input.GetAxisRaw("Mouse X");

            Vector3 movement = target.up * y;
            movement += target.right * x;
            transform.Translate(movement * moveSensitivity, Space.World);
        }

        private void OnValidate() => Visualize();

        private void Visualize()
        {
            target.localPosition = position;
            transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}
