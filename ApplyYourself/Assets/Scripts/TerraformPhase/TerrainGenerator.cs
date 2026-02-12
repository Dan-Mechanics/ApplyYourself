using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class TerrainGenerator : MonoBehaviour, IHeightmap 
    {
        [SerializeField] private GameObject chunkPrefab = default;
        [SerializeField] private int chunksAcross = default;
        [SerializeField] private Texture2D heightmapTexture = default;
        [SerializeField] private float spaceBetweenChunks = default;
        [SerializeField] private float height = default;

        // spatial hash !!
        private List<Chunk> chunks = new List<Chunk>();

        private void Start()
        {
            for (int x = 0; x < chunksAcross; x++)
            {
                for (int z = 0; z < chunksAcross; z++)
                {
                    GameObject go = Instantiate(chunkPrefab, Vector3.zero, Quaternion.identity);
                    Chunk chunk = go.GetComponent<Chunk>();
                    go.name = $"chunk_({x}, {z})";

                    Vector3 offset = new Vector3(spaceBetweenChunks * x, 0f, spaceBetweenChunks * z);
                    chunk.Setup(this, offset);
                    chunks.Add(chunk);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                chunks.ForEach(x => x.MoveUp());
        }

        public float GetHeight(float worldX, float worldZ)
        {
            int x = Mathf.FloorToInt(worldX);
            int y = Mathf.FloorToInt(worldZ);
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