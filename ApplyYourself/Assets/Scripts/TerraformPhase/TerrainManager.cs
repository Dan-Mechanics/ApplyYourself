using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class TerrainManager : MonoBehaviour, IHeightmap 
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                ReloadAll();
        }

        /// <summary>
        /// TODO: assign to button.
        /// </summary>
        private void ReloadAll()
        {
            Dictionary<Vector2Int, float> heightmap = new Dictionary<Vector2Int, float>();
            for (int x = 0; x < chunksAcross; x++)
            {
                for (int z = 0; z < chunksAcross; z++)
                {
                    Chunk chunk = chunks[new Vector2Int(x, z)];
                    for (int i = 0; i < chunk.verticies.Length; i++)
                    {
                        Vector3 vert = chunk.verticies[i];
                        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(vert.x), Mathf.RoundToInt(vert.z));
                        if (heightmap.ContainsKey(pos))
                        {
                            vert.y = heightmap[pos];
                        }
                        else
                        {
                            heightmap[pos] = vert.y;
                        }

                        chunk.verticies[i] = vert;
                    }

                    chunk.ReloadMesh();
                }
            }
        }

        public void ClearChunks()
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Chunk");
            for (int i = 0; i < gameObjects.Length; i++)
            {
                DestroyImmediate(gameObjects[i]);
            }

            chunks.Clear();
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                chunks.ForEach(x => x.MoveUp());
        }*/

        public List<Chunk> GetChunksInProximity(Vector3 point) 
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
            x = Mathf.Clamp(x, 0, heightmapTexture.width - 1);
            y = Mathf.Clamp(y, 0, heightmapTexture.height - 1);

            return heightmapTexture.GetPixel(x, y).r * height;
        }

        private void OnDrawGizmos()
        {
            if (chunks.Count <= 0)
                return;
            
            Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
            for (int x = 0; x < chunksAcross; x++)
            {
                for (int z = 0; z < chunksAcross; z++)
                {
                    Vector3 pos = new Vector3(x * spaceBetweenChunks, 0f, z * spaceBetweenChunks);
                    pos += 0.5f * spaceBetweenChunks * Vector3.one;
                    Gizmos.DrawWireCube(pos, Vector3.one * spaceBetweenChunks);
                }
            }
        }
    }
}