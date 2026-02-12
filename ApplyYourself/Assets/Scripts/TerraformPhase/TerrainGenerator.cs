using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class TerrainGenerator : MonoBehaviour, IHeightmap 
    {
        [SerializeField] private GameObject chunkPrefab = default;
        [SerializeField] private Texture2D heightmapTexture = default;
        [SerializeField] private float heightmapScale = default;
        [SerializeField] private float height = default;
        [SerializeField] private int chunksAcross = default;
        [SerializeField] private float spaceBetweenChunks = default;

        // spatial hash !!
        private readonly List<Chunk> chunks = new List<Chunk>();

        //private readonly Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();

        private void Start() => Setup();

        public void Setup()
        {
            Clear();
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

        public void Clear()
        {
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i] != null)
                    DestroyImmediate(chunks[i].gameObject);
            }

            chunks.Clear();
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                chunks.ForEach(x => x.MoveUp());
        }*/

        public float GetHeight(float worldX, float worldZ)
        {
            int x = Mathf.RoundToInt(worldX / heightmapScale);
            int y = Mathf.RoundToInt(worldZ / heightmapScale);
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