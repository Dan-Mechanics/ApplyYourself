using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Working on this.
    /// </summary>
    public class PivotController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private float sens = default;
        [SerializeField] private float zoomSens = default;
        [SerializeField] private float minDistance = default;
        [SerializeField] private float maxDistance = default;
        [SerializeField] private Vector3 position = default;
        [SerializeField] private Vector3 rotation = default;

        private void Update()
        {
            Cursor.visible = !Input.GetKey(KeyCode.Mouse1);
            if (Input.GetKey(KeyCode.Mouse1))
            {
                rotation.y += sens * Input.GetAxisRaw("Mouse X");
                rotation.x -= sens * Input.GetAxisRaw("Mouse Y");
                rotation.x = Mathf.Clamp(rotation.x, 7.5f, 90f);
            }

            position.z += Input.mouseScrollDelta.y * zoomSens;
            position.z = Mathf.Clamp(position.z, -maxDistance, -minDistance);

            transform.localRotation = Quaternion.Euler(rotation);
            target.localPosition = position;
        }

        private void OnValidate()
        {
            target.localPosition = position;
            transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}
