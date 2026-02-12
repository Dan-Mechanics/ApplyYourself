using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class TerrainManager : MonoBehaviour
    {
        [SerializeField] private GameObject chunkPrefab = default;
        [SerializeField] private Texture2D heightmapTexture = default;
        [SerializeField] private float height = default;
        [SerializeField] private int vertsAcrossChunk = default;
       // [SerializeField] private float spacing = default;
        [SerializeField] private int chunksAcross = default;
        [SerializeField] private bool showInEditor = default;

     //  private Chunk[,] chunks;

        private void OnValidate()
        {
            if (showInEditor)
                Setup();

            showInEditor = false;
        }

        public void Setup()
        {

           // chunks = new Chunk[chunksAcross, chunksAcross];
            for (int x = 0; x < chunksAcross; x++)
            {
                for (int z = 0; z < chunksAcross; z++)
                {
                    Vector3 offset = new Vector3(x * vertsAcrossChunk, 0f, z * vertsAcrossChunk);
                    GameObject go = Instantiate(chunkPrefab, offset, Quaternion.identity);
                    Chunk chunk = go.GetComponent<Chunk>();

                    go.name = $"chunk_{offset}";
                    chunk.Setup(this, vertsAcrossChunk);
                   // chunks[x, z] = chunk;
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
