using UnityEngine;

namespace ApplyYourself
{
    public class PlayerMovement : StateBehaviour
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private Transform leftRightPivot = default;
        [SerializeField] private float speed = default;
        [SerializeField] private float fallingSpeed = default;

        public override void OnUpdate()
        {
            base.OnUpdate();
            Vector3 movement = (Input.GetAxisRaw("Horizontal") * leftRightPivot.right) +
                (Input.GetAxisRaw("Vertical") * leftRightPivot.forward);

            movement.Normalize();
            movement *= speed;

            controller.Move(movement * Time.deltaTime);
            controller.Move(fallingSpeed * Time.deltaTime * Vector3.down);
        }
    }
}
