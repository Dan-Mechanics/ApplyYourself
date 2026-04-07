using UnityEngine;
using UnityEngine.Events;

namespace ApplyYourself
{
    public class PlayerMovement : StateBehaviour
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private Transform leftRightPivot = default;
        [SerializeField] private float speed = default;
        [SerializeField] private float fallingSpeed = default;
        [SerializeField] private UnityEvent onWalk = default;
        [SerializeField] private UnityEvent onIdle = default;
        private IMoveInput moveInput;
        private Vector3 prevMovement;

        public void Assign(IMoveInput moveInput)
        {
            this.moveInput = moveInput;
            onIdle?.Invoke();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Vector3 movement = moveInput.GetMove();
            movement = leftRightPivot.TransformDirection(movement);
            movement *= speed;

            if (movement == Vector3.zero && prevMovement != Vector3.zero) 
                onIdle?.Invoke();

            if (movement != Vector3.zero && prevMovement == Vector3.zero)
                onWalk?.Invoke();

            controller.Move(movement * Time.deltaTime);
            prevMovement = movement;
        }

        private void Update()
        {
            controller.Move(fallingSpeed * Time.deltaTime * Vector3.down);
        }

        public override void Exit()
        {
            base.Exit();
            onIdle?.Invoke();
            prevMovement = Vector3.zero;
        }

        public void Teleport(Vector3 position)
        {
            controller.enabled = false;
            transform.position = position;
            controller.enabled = true;
        }
    }
}
