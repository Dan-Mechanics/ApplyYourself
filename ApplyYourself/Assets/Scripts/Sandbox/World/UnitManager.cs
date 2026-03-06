using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ApplyYourself
{
    public class UnitManager : MonoBehaviour 
    {
        public event Action<float, float> OnNewWaterRange; 
        
        [SerializeField] private int width = default;
        [SerializeField] private float visualWaterShake = default;
        [SerializeField] private float waterGradientInterval = default;
        [SerializeField] private UnitType water = default;
        [SerializeField] private GridSpawner spawner = default;

        private ITypemap typemap;
        private IHeightmap landManager;
        private IHeightmap waterManager;
        private UnitVisual[,] units;
        private float next;

        public void Initialize(ITypemap typemap, IHeightmap landManager, IHeightmap waterManager)
        {
            Terminate();

            this.typemap = typemap;
            this.landManager = landManager;
            this.waterManager = waterManager;

            GameObject[,] grid = spawner.SpawnGrid(width);
            units = new UnitVisual[width, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Transform unit = grid[x, y].transform;
                    units[x, y].Assign(unit, unit.GetChild(0).GetComponent<MeshRenderer>());
                    units[x, y].SetDecoration(typemap.GetTypeAt(x, y).decoration);
                }
            }
        }

        private void FixedUpdate()
        {
            if (Time.time < next)
                return;

            next = Time.time + waterGradientInterval;
            RecalculateWaterGradient();
        }

        public void RenderArea(List<Vector2Int> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;
                RenderUnit(x, y);
            }
        }

        public void RedecorateArea(List<Vector2Int> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;

                units[x, y].SetDecoration(typemap.GetTypeAt(x, y).decoration);
                //RenderUnit(x, y);
            }
        }

        public void RenderAll()
        {
            // IF THIS MAKES A COPY, COMPUTER EXPLODES.
            float[,] landBulk = landManager.GetBulk();
            float[,] waterBulk = waterManager.GetBulk(); 
            UnitType[,] typeBulk = typemap.GetBulk();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    // RenderUnit(x, y);
                    // IDK WHAT IS FASTER HERE.
                    RenderUnitBulk(x, y, landBulk, waterBulk, typeBulk);
                }
            }
        }

        private void RecalculateWaterGradient()
        {
            float highestWater = waterManager.GetHeightAt(0, 0);
            float lowestWater = highestWater;

            bool skip = false;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    skip = !skip;
                    if (skip)
                        continue;

                    float waterHeight = waterManager.GetHeightAt(x, y);
                    if (landManager.GetHeightAt(x, y) >= waterHeight)
                        continue;

                    if (waterHeight < lowestWater)
                        lowestWater = waterHeight;
                    else if (waterHeight > highestWater)
                        highestWater = waterHeight;
                }
            }

            OnNewWaterRange?.Invoke(lowestWater, highestWater);
        }

        private void RenderUnit(int x, int y)
        {
            float landHeight = landManager.GetHeightAt(x, y);
            float waterHeight = waterManager.GetHeightAt(x, y);
            bool isLand = landHeight >= waterHeight;
            if (isLand)
            {
                units[x, y].Set(typemap.GetTypeAt(x, y).material, isLand, landHeight);
            }
            else
            {
                units[x, y].Set(water.material, isLand, waterHeight + (UnityEngine.Random.value - 0.5f) * visualWaterShake);
            }
        }

        private void RenderUnitBulk(int x, int y, float[,] landBulk, float[,] waterBulk, UnitType[,] typeBulk)
        {
            float landHeight = landBulk[x, y];
            float waterHeight = waterBulk[x, y];
            bool isLand = landHeight >= waterHeight;
            if (isLand)
            {
                units[x, y].Set(typeBulk[x, y].material, isLand, landHeight);
            }
            else
            {
                units[x, y].Set(water.material, isLand, waterHeight + (UnityEngine.Random.value - 0.5f) * visualWaterShake);
            }
        }

        public void Terminate()
        {
            GameObject.FindGameObjectsWithTag("Unit").
                ToList().ForEach(x => DestroyImmediate(x));
        }
    }
}