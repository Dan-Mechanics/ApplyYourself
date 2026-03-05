using UnityEngine;

namespace ApplyYourself
{
    public class PlayerGraphicRotator : StateBehaviour
    {
        [SerializeField] private Transform leftRightPivot = default;
        [SerializeField] private Transform graphic = default;
        private Transform heading;

        public override void Setup()
        {
            base.Setup();
            heading = new GameObject(nameof(heading)).transform;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            // USEI NTERFACE !!
            Vector3 movement = (Input.GetAxisRaw("Horizontal") * leftRightPivot.right) +
                (Input.GetAxisRaw("Vertical") * leftRightPivot.forward);

            movement.Normalize();

            if (movement == Vector3.zero)
                return;

            heading.position = transform.position + movement;
            graphic.LookAt(heading);
        }
    }
}
