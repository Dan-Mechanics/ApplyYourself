using UnityEngine;

namespace ApplyYourself
{
    public class PlayerGraphicRotator : StateBehaviour
    {
        [SerializeField] private Transform leftRightPivot = default;
        [SerializeField] private Transform graphic = default;
        private IMoveInput moveInput;
        private Transform heading;

        public void Assign(IMoveInput moveInput)
        {
            this.moveInput = moveInput;
            heading = new GameObject(nameof(heading)).transform;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Vector3 movement = moveInput.GetMove();
            movement = leftRightPivot.TransformDirection(movement);
            if (movement == Vector3.zero)
                return;

            heading.position = transform.position + movement;
            graphic.LookAt(heading);
        }
    }
}
