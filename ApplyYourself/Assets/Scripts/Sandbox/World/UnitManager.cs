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

        private UnitType[,] landTypemap;
        private float[,] landHeightmap;
        private float[,] waterHeightmap;

        private UnitVisual[,] units;
        private float next;

        public void Setup(UnitType[,] landTypemap, float[,] landHeightmap, float[,] waterHeightmap)
        {
            Clear();

            this.landTypemap = landTypemap;
            this.landHeightmap = landHeightmap;
            this.waterHeightmap = waterHeightmap;

            GameObject[,] grid = spawner.SpawnGrid(width);
            units = new UnitVisual[width, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Transform unit = grid[x, y].transform;
                    units[x, y].Assign(unit, unit.GetChild(0).GetComponent<MeshRenderer>());
                    units[x, y].SetDecoration(landTypemap[x, y].decoration);
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

        public void RenderAreaDecoration(List<Vector2Int> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;

                units[x, y].SetDecoration(landTypemap[x, y].decoration);
                RenderUnit(x, y);
            }
        }

        public void RenderAll()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    RenderUnit(x, y);
                }
            }
        }

        private void RecalculateWaterGradient()
        {
            float highestWater = waterHeightmap[0, 0];
            float lowestWater = highestWater;

            bool skip = false;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    skip = !skip;
                    if (skip)
                        continue;

                    float waterHeight = waterHeightmap[x, y];
                    if (landHeightmap[x, y] >= waterHeight)
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
            float landHeight = landHeightmap[x, y];
            float waterHeight = waterHeightmap[x, y];
            bool isLand = landHeight >= waterHeight;
            if (isLand)
            {
                units[x, y].Set(landTypemap[x, y].material, isLand, landHeight);
            }
            else
            {
                units[x, y].Set(water.material, isLand, waterHeight + (UnityEngine.Random.value - 0.5f) * visualWaterShake);
            }
        }

        public void Clear()
        {
            GameObject.FindGameObjectsWithTag("Unit").
                ToList().ForEach(x => DestroyImmediate(x));
        }
    }
}