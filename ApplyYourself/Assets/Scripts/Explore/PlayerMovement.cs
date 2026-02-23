using UnityEngine;

namespace ApplyYourself
{
    public class PlayerMovement : StateBehaviour
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private EasyBinding fly = default;
        [SerializeField] private Transform graphic = default;
        [SerializeField] private Transform heading = default;
        [SerializeField] private Vector3 flyVelocity = default;
        [SerializeField] private float speed = default;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            Vector3 movement = (Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward);
            movement.Normalize();
            movement *= speed;

            if (movement != Vector3.zero)
            {
                heading.position = transform.position + movement;
                graphic.LookAt(heading);
            }

            controller.Move(movement * Time.fixedDeltaTime);

            if (fly.IsHeld)
            {
                controller.Move(flyVelocity * Time.fixedDeltaTime);
            }
            else
            {
                controller.Move(Physics.gravity * Time.fixedDeltaTime);
            }
        }
    }
}
