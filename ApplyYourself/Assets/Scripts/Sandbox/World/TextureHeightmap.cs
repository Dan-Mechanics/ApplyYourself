using UnityEngine;

namespace ApplyYourself
{
    public class TextureHeightmap : MonoBehaviour, IHeightmap 
    {
        [SerializeField] private Texture2D heightmap = default;
        [SerializeField] private float height = default;

        public float GetHeight(int x, int y)
        {
            x = Mathf.Clamp(x, 0, heightmap.width - 1);
            y = Mathf.Clamp(y, 0, heightmap.height - 1);
            return heightmap.GetPixel(x, y).r * height;
        }
    }
}