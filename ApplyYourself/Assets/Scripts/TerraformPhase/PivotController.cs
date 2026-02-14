using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// TODOOD !!! Add state behaviour gag
    /// </summary>
    public class PivotController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private Transform cam = null;

        [Header("Rotation")]
        [SerializeField] private Vector3 rotation = default;
        [SerializeField] private float sens = default;
        [SerializeField] private float minAngle = default;
        [SerializeField] private float maxAngle = default;

        [Header("Zoom")]
        [SerializeField] private Vector3 position = default;
        [SerializeField] private float zoomSens = default;
        [SerializeField] private float minDistance = default;
        [SerializeField] private float maxDistance = default;

        [Header("Position")]
        [SerializeField] private float movingSens = default;

        private void Update()
        {
            Cursor.visible = !Input.GetKey(KeyCode.Mouse1);
            if (Input.GetKey(KeyCode.Mouse2))
            {
                Move();
                return;
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                Rotate();
            }

            Scroll();
            Refresh();
        }

        private void Scroll()
        {
            position.z += Input.mouseScrollDelta.y * zoomSens;
            position.z = Mathf.Clamp(position.z, -maxDistance, -minDistance);
        }

        private void Rotate()
        {
            rotation.y += sens * Input.GetAxisRaw("Mouse X");
            rotation.x -= sens * Input.GetAxisRaw("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, minAngle, maxAngle);
        }

        private void Move()
        {
            float y = -Input.GetAxisRaw("Mouse Y");
            float x = -Input.GetAxisRaw("Mouse X");

            Vector3 movement = cam.up * y;
            movement += cam.right * x;
            transform.Translate(movement * movingSens, Space.World);
        }

        private void OnValidate() => Refresh();

        private void Refresh()
        {
            target.localPosition = position;
            transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}
