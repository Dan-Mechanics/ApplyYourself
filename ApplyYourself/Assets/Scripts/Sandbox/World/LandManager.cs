using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ApplyYourself
{
    public class LandManager : MonoBehaviour 
    {
        [SerializeField] private int width = default;
        [SerializeField] private float minHeight = default;
        [SerializeField] private float maxHeight = default;
        [SerializeField] private GridSpawner spawner = default;
        [SerializeField] private UnitType water = default;
        [SerializeField] private UnitType city = default;
        [SerializeField] private UnitType plains = default;
        [SerializeField] private float waterHeight = default;

        private IHeightmap heightmap;
        private ITypemap typemap;
        private UnitVisual[,] unitVisuals;
        private UnitType[,] unitTypes;
        private float[,] unitHeights;

        private void Start() => Initialize();

        public void Initialize()
        {
            heightmap = GetComponent<IHeightmap>();
            typemap = GetComponent<ITypemap>();

            GameObject[,] grid = spawner.SpawnGrid(width);
            unitHeights = new float[width, width];
            unitTypes = new UnitType[width, width];
            unitVisuals = new UnitVisual[width, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    // IMPORTANT: CLAMP WHEN CHANGEN NOT JUST ALL THE TIME.
                    unitHeights[x, y] = heightmap.GetHeightAt(x, y);
                    unitTypes[x, y] = typemap.GetTypeAt(x, y);

                    Transform unit = grid[x, y].transform;
                    unitVisuals[x, y].Assign(unit, unit.GetChild(0).GetComponent<MeshRenderer>());
                    unitVisuals[x, y].SetDecoration(unitTypes[x, y].decoration);
                }
            }

            Tick();
        }

        public void RaiseArea(List<Vector2Int> positions, float motion)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;
                unitHeights[x, y] = Mathf.Clamp(unitHeights[x, y] + motion, minHeight, maxHeight);
            }
        }

        public void DecorateArea(List<Vector2Int> positions, UnitType type)
        {
            if (type == null)
                type = plains;

            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;

                // ??
                if (unitHeights[x, y] < waterHeight || unitTypes[x, y] == type || unitTypes[x, y] == city)
                    continue;

                unitTypes[x, y] = type;
                unitVisuals[x, y].SetDecoration(type.decoration);
            }
        }

       //public float GetHeightAt(int x, int y) => unitHeights[x, y];
       //public UnitType GetTypeAt(int x, int y) => unitTypes[x, y];

        /// <summary>
        /// Called by SandboxManager.
        /// </summary>
        public void Tick()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    float landHeight = unitHeights[x, y];
                    bool isLand = landHeight >= waterHeight;
                    if (isLand)
                    {
                        // YOU COULD MAKE THIS ONE METHOD.
                        unitVisuals[x, y].SetMaterial(typemap.GetTypeAt(x, y).material);
                        unitVisuals[x, y].EnableDecoration(true);
                        unitVisuals[x, y].SetHeight(landHeight);
                    }
                    else 
                    {
                        unitVisuals[x, y].SetMaterial(water.material);
                        unitVisuals[x, y].EnableDecoration(false);
                        unitVisuals[x, y].SetHeight(waterHeight);
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