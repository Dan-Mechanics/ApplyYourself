using System.Linq;
using UnityEngine;

namespace ApplyYourself
{
    public class UnitManager : MonoBehaviour
    {
        
        /// <summary>
        /// De[ednancy injkection
        /// </summary>
        [SerializeField] private LandManager landManager = default;
        [SerializeField] private WaterManager waterManager = default;

        [SerializeField] private int width = default;
        [SerializeField] private GridSpawner spawner = default;

        private UnitVisual[,] unitVisuals;

        private void Start() => Initialize();

        public void Initialize()
        {
            GameObject[,] grid = spawner.SpawnGrid(width);
            unitVisuals = new UnitVisual[width, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Transform unit = grid[x, y].transform;
                    unitVisuals[x, y].Assign(unit, unit.GetChild(0).GetComponent<MeshRenderer>());
                }
            }
        }

        private void Tick()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    // compare hiehgt.

                    float landHeight = landManager.GetHeightAt(x, y);
                    float waterHeight = waterManager.GetHeightAt(x, y);
                    if (landHeight > waterHeight)
                    {

                    }
                }
            }
        }

        public void Terminate()
        {
            GameObject.FindGameObjectsWithTag("Chunk").
                ToList().ForEach(x => DestroyImmediate(x));
        }
    }
}
