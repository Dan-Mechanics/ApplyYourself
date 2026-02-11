using UnityEngine;

namespace ApplyYourself
{
    public class TerrainManager : MonoBehaviour
    {
        [SerializeField] private GameObject chunkPrefab = default;
        [SerializeField] private int chunkSize = default;
        [SerializeField] private int chunkCount = default;

        private IHeightmap heightmap;
        
        private void Awake()
        {
            heightmap = GetComponent<IHeightmap>();

        }

        private void Start()
        {
            // spawn chunks.

            for (int x = 0; x < chunkSize; x++)
            {
                for (int z = 0; z < chunkSize; z++)
                {

                }
            }
        }
    }
}