using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class TerrainGenerator : MonoBehaviour, IHeightmap 
    {
        public readonly Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();

        [SerializeField] private GameObject chunkPrefab = default;
        [SerializeField] private Texture2D heightmapTexture = default;
        [SerializeField] private float heightmapScale = default;
        [SerializeField] private float height = default;
        [SerializeField] private int chunksAcross = default;
        [SerializeField] private float spaceBetweenChunks = default;

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
                    chunks[new Vector2Int(x, z)] = chunk;
                }
            }
        }

        public void Clear()
        {
            foreach (var pair in chunks)
            {
                var chunk = pair.Value;
                if(chunk != null)
                    DestroyImmediate(chunk.gameObject);
            }

            chunks.Clear();
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                chunks.ForEach(x => x.MoveUp());
        }*/

        public List<Chunk> GetChunks(Vector3 point) 
        {
            List<Chunk> result = new List<Chunk>();
            float max = spaceBetweenChunks * chunksAcross;
            point.x = Mathf.Clamp(point.x, 0f, max);
            point.z = Mathf.Clamp(point.z, 0f, max);

            Vector2Int middle = Utils.GetCellPos(new Vector2(point.x, point.z), spaceBetweenChunks);
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Vector2Int key = middle + new Vector2Int(x, z);
                    if (chunks.ContainsKey(key))
                        result.Add(chunks[key]);
                }
            }

            return result;
        }

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