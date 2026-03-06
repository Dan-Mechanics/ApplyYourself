using UnityEngine;

namespace ApplyYourself
{
    public class TextureHeightmap : MonoBehaviour, IHeightmap 
    {
        [SerializeField] private Texture2D heightmap = default;
        [SerializeField] private float height = default;

        public float[,] GetBulk()
        {
            float[,] grid = new float[heightmap.width, heightmap.height];
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = GetHeightAt(x, y);
                }
            }

            return grid;
        }

        public float GetHeightAt(int x, int y)
        {
            x = Mathf.Clamp(x, 0, heightmap.width - 1);
            y = Mathf.Clamp(y, 0, heightmap.height - 1);
            return heightmap.GetPixel(x, y).r * height;
        }
    }
}