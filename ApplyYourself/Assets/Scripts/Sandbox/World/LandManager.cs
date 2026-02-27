using System.Linq;
using UnityEngine;

namespace ApplyYourself
{
    public class LandManager : StateBehaviour 
    {
        [SerializeField] private int width = default;
        [SerializeField] private float minHeight = default;
        [SerializeField] private float maxHeight = default;
        [SerializeField] private GridSpawner spawner = default;

        private IHeightmap heightmap;
        private ITypemap typemap;
        private UnitVisual[,] unitVisuals;
        private UnitType[,] unitTypes;
        private float[,] unitHeights;

        public override void Enter()
        {
            base.Enter();
            Initialize();
        }

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
                    unitHeights[x, y] = Mathf.Clamp(heightmap.GetHeight(x, y), minHeight, maxHeight);
                    unitTypes[x, y] = typemap.GetType(x, y);

                    Transform unit = grid[x, y].transform;
                    unitVisuals[x, y].Assign(unit, unit.GetChild(0).GetComponent<MeshRenderer>());
                }
            }

            UpdateAllHeights();
            UpdateAllTypes();
        }

        private void UpdateAllHeights()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Vector3 pos = unitVisuals[x, y].transform.position;
                    pos.y = unitHeights[x, y];
                    unitVisuals[x, y].transform.position = pos;
                }
            }
        }

        private void UpdateAllTypes()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    unitVisuals[x, y].SetAs(unitTypes[x, y]);
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