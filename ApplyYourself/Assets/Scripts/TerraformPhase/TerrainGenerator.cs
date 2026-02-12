using UnityEngine;

namespace ApplyYourself
{
    public class TerrainGenerator : MonoBehaviour, IHeightmapService 
    {
        [SerializeField] private GameObject chunkPrefab = default;
        [SerializeField] private int chunksAcross = default;
        [SerializeField] private Texture2D heightmapTexture = default;
        [SerializeField] private float spaceBetweenChunks = default;
        [SerializeField] private float height = default;

        private void Start()
        {
            for (int x = 0; x < chunksAcross; x++)
            {
                for (int z = 0; z < chunksAcross; z++)
                {
                    GameObject go = Instantiate(chunkPrefab, new Vector3(spaceBetweenChunks * x, 0f, spaceBetweenChunks * z), Quaternion.identity);
                    go.GetComponent<Chunk>().Setup(this);
                }
            }
        }

        public float GetHeight(float worldX, float worldZ)
        {
            int x = Mathf.RoundToInt(worldX);
            int y = Mathf.RoundToInt(worldZ);
            if (x < 0)
                x = 0;

            if (y < 0)
                y = 0;

            if (x > heightmapTexture.width - 1)
                x = heightmapTexture.width - 1;

            if (y > heightmapTexture.height - 1)
                y = heightmapTexture.height - 1;

            return heightmapTexture.GetPixel(x, y).r * height;
        }
    }
}