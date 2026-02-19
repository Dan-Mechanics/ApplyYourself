using UnityEngine;

namespace ApplyYourself
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private Transform graphic = default;
        [SerializeField] private Transform heading = default;
        [SerializeField] private float speed = default;

        private void FixedUpdate()
        {
            Vector3 movement = (Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward);
            movement.Normalize();
            movement *= speed;

            if (movement != Vector3.zero)
            {
                heading.position = transform.position + movement;
                graphic.LookAt(heading);
            }

            controller.Move(movement * Time.fixedDeltaTime);
            controller.Move(Physics.gravity * Time.fixedDeltaTime);
        }
    }
}
