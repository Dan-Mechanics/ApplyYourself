using UnityEngine;

namespace ApplyYourself
{
    public class TerrainManager : Grid 
    {
        [SerializeField] private float height = default;

        public override float GetHeight(int x, int z)
        {
            return heightmap.GetPixel(x, z).r * height;
        }
    }
}