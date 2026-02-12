using UnityEngine;

namespace ApplyYourself
{
    public class TerrainMaterial : MonoBehaviour
    {
        [SerializeField] private Renderer rend = default;
        [SerializeField] private Material material = default;
        [SerializeField] private Texture2D texture = default;
        [SerializeField] private float worldFloorHeight = default;
        [SerializeField] private float worldCeilingHeight = default;

        private void Start()
        {
            Setup();
            Destroy(this);
        }

        private void Setup()
        {
            rend.material = material;
            material.SetFloat("_WorldFloorHeight", worldFloorHeight);
            material.SetFloat("_WorldCeilingHeight", worldCeilingHeight);
            material.SetTexture("_Texture", texture);
        }
    }
}