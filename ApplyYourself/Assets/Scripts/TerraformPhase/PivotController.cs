using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// TODOOD !!! Add state behaviour gag
    /// </summary>
    public class PivotController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private float sens = default;
        [SerializeField] private float zoomSens = default;
        [SerializeField] private float movingSens = default;
        [SerializeField] private float minDistance = default;
        [SerializeField] private float maxDistance = default;
        [SerializeField] private float minAngle = default;
        [SerializeField] private float maxAngle = default;
        [SerializeField] private Vector3 position = default;
        [SerializeField] private Vector3 rotation = default;

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
            float y = movingSens * Input.GetAxisRaw("Mouse Y");
            float x = movingSens * Input.GetAxisRaw("Mouse X");

            transform.Translate(transform.up * y, Space.Self);
            transform.Translate(transform.right * x, Space.Self);
        }

        private void OnValidate() => Refresh();

        private void Refresh()
        {
            target.localPosition = position;
            transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}
