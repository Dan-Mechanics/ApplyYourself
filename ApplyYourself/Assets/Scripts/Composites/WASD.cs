using UnityEngine;

namespace ApplyYourself
{
    public class WASD : IMoveInput
    {
        public Vector3 GetMove()
        {
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            return movement.normalized;
        }
    }
}
