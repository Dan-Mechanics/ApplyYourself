using UnityEngine;

namespace ApplyYourself
{
    public class PlayerMovement : StateBehaviour
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private Transform leftRightPivot = default;
        [SerializeField] private float speed = default;
        [SerializeField] private float fallingSpeed = default;
        private IMoveInput moveInput;

        public void Assign(IMoveInput moveInput) => this.moveInput = moveInput;

        public override void OnUpdate()
        {
            base.OnUpdate();
            Vector3 movement = moveInput.GetMove();
            movement = leftRightPivot.TransformDirection(movement);
            movement *= speed;

            controller.Move(movement * Time.deltaTime);
            controller.Move(fallingSpeed * Time.deltaTime * Vector3.down);
        }
    }
}
