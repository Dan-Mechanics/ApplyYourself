using UnityEngine;

namespace ApplyYourself
{
    public class LoopMaterial : MonoBehaviour
    {
        [SerializeField] private Material material = default;
        [SerializeField] private Vector2 velocity = default;

        private void FixedUpdate()
        {
            material.mainTextureOffset += velocity * Time.fixedDeltaTime;
        }
    }
}
