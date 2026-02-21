using UnityEngine;

namespace ApplyYourself
{
    public class WaterManager : GridComposite 
    {
        [SerializeField] private float height = default;
        [SerializeField] private float offset = default;
        [SerializeField] private float scale = default;

        public override float GetHeight(int x, int z)
        {
            float worldX = x * chunkSize;
            float worldZ = z * chunkSize;
            return GetHeight(worldX, worldZ);
        }

        private float GetHeight(float worldX, float worldZ)
        {
            worldX += Time.time;
            worldZ += Time.time * 0.8f;
            return Mathf.PerlinNoise(worldX * scale, worldZ * scale) * height + offset;
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                Vector3 pos = transforms[i].position;
                pos.y = GetHeight(pos.x, pos.z);
                transforms[i].position = pos;
            }
        }
    }
}