using UnityEngine;

namespace ApplyYourself
{
    public class AdaptiveGradient : MonoBehaviour
    {
        [SerializeField] private Material material = default;
        [SerializeField] private Texture2D texture = default;
        [SerializeField] private float worldFloorHeight = default;
        [SerializeField] private float worldCeilingHeight = default;

        private void Start() => Setup();
        private void OnValidate() => Setup();

        private void Setup()
        {
            material.SetFloat("_WorldFloorHeight", worldFloorHeight);
            material.SetFloat("_WorldCeilingHeight", worldCeilingHeight);
            material.SetTexture("_Texture", texture);
        }

        public void SetRange(float min, float max) 
        {
            worldFloorHeight = min;
            worldCeilingHeight = max;
            material.SetFloat("_WorldFloorHeight", worldFloorHeight);
            material.SetFloat("_WorldCeilingHeight", worldCeilingHeight);
        }
    }
}