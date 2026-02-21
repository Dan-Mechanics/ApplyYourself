using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Todo: use composition over inhertience.
    /// Call it grid and make it a class.
    /// </summary>
    public abstract class GridComposite : StateBehaviour 
    {
        [SerializeField] private GameObject prefab = default;
        [SerializeField] protected Texture2D heightmap = default;
        [SerializeField] protected float chunkSize = default;

        protected Transform[] transforms;
        protected int width;

        /// <summary>
        /// This needs to be an animation.
        /// </summary>
        private void SpawnGrid()
        {
            width = heightmap.width;
            transforms = new Transform[width * width];
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < width; z++)
                {
                    Vector3 position = new Vector3(x * chunkSize, GetHeight(x , z), z * chunkSize);
                    position.x += 0.5f * chunkSize;
                    position.z += 0.5f * chunkSize;
                        
                    GameObject go = Instantiate(prefab, position, Quaternion.identity);
                    go.name = prefab.name;
                    transforms[x + z * width] = go.transform;
                }
            }
        }

        public abstract float GetHeight(int x, int z);

        public override void Enter()
        {
            base.Enter();
            SpawnGrid();
        }

        public void ClearChunks()
        {
            transforms = null;
            GameObject[] chunks = GameObject.FindGameObjectsWithTag("Chunk");
            for (int i = 0; i < chunks.Length; i++)
            {
                DestroyImmediate(chunks[i]);
            }
        }

        public float[] GetHeights()
        {
            float[] heights = new float[transforms.Length];
            for (int i = 0; i < heights.Length; i++)
            {
                heights[i] = transforms[i].position.y;
            }

            return heights;
        }
    }
}