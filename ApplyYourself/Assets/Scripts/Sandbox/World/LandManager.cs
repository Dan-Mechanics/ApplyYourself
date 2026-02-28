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
       // [SerializeField] private float waterHeight = default;
        [SerializeField] private WaterManager waterManager = default;
        [SerializeField] private UnitType water = default;
        [SerializeField] private UnitType city = default;
        [SerializeField] private UnitType plains = default;
        [SerializeField] private GridSpawner spawner = default;

        private UnitVisual[,] unitVisuals;
        private UnitType[,] unitTypes;
        private float[,] unitHeights;

        //private void Start() => Initialize();

        public void Initialize()
        {
            // transform.Find("dwdw") here
            IHeightmap startupHeightmap = GetComponent<IHeightmap>();
            ITypemap startupTypemap = GetComponent<ITypemap>();

            GameObject[,] grid = spawner.SpawnGrid(width);
            unitHeights = new float[width, width];
            unitTypes = new UnitType[width, width];
            unitVisuals = new UnitVisual[width, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    // IMPORTANT: CLAMP WHEN CHANGEN NOT JUST ALL THE TIME.
                    unitHeights[x, y] = startupHeightmap.GetHeightAt(x, y);
                    unitTypes[x, y] = startupTypemap.GetTypeAt(x, y);

                    Transform unit = grid[x, y].transform;
                    unitVisuals[x, y].Assign(unit, unit.GetChild(0).GetComponent<MeshRenderer>());
                    unitVisuals[x, y].SetDecoration(unitTypes[x, y].decoration);
                }
            }

            //Render();
        }

        public void RaiseArea(List<Vector2Int> positions, float motion)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;
                unitHeights[x, y] = Mathf.Clamp(unitHeights[x, y] + motion, minHeight, maxHeight);
                RenderUnit(x, y);
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

                // ?? !!
                if (unitHeights[x, y] < waterManager.GetHeightAt(x, y)|| unitTypes[x, y] == type || unitTypes[x, y] == city)
                    continue;

                unitTypes[x, y] = type;
                unitVisuals[x, y].SetDecoration(type.decoration);
                RenderUnit(x, y);
            }
        }

        public float GetHeightAt(int x, int y) => unitHeights[x, y];
        public UnitType GetTypeAt(int x, int y) => unitTypes[x, y];

        /// <summary>
        /// Called by SandboxManager.
        /// </summary>
        public void Render()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    RenderUnit(x, y);
                }
            }
        }

        private void RenderUnit(int x, int y)
        {
            float landHeight = unitHeights[x, y];
            float waterHeight = waterManager.GetHeightAt(x, y);
            bool isLand = landHeight >= waterHeight;

            if (isLand)
            {
                // YOU COULD MAKE THIS ONE METHOD.
                unitVisuals[x, y].SetMaterial(unitTypes[x, y].material);
                unitVisuals[x, y].EnableDecoration(true);
                unitVisuals[x, y].SetHeight(landHeight);
            }
            else
            {
                unitVisuals[x, y].SetMaterial(water.material);
                unitVisuals[x, y].EnableDecoration(false);
                unitVisuals[x, y].SetHeight(waterHeight + Random.value);
            }
        }

        public void Terminate()
        {
            GameObject.FindGameObjectsWithTag("Chunk").
                ToList().ForEach(x => DestroyImmediate(x));
        }
    }
}