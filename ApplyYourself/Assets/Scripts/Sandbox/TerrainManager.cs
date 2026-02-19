using UnityEngine;

namespace ApplyYourself
{
    public class TerrainManager : GridManager 
    {
        [SerializeField] private float height = default;

        public override float GetHeight(int x, int z)
        {
            return heightmap.GetPixel(x, z).r * height;
        }
    }
}