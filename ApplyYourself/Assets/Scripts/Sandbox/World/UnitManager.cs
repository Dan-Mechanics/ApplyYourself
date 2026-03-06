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
        [SerializeField] private UnitType water = default;
        [SerializeField] private GridSpawner spawner = default;

        private ITypemap typemap;
        private IHeightmap landManager;
        private IHeightmap waterManager;
        private UnitVisual[,] units;
        private float lowestWater;
        private float highestWater;

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

        /// <summary>
        /// Called by InvokeRepeating.
        /// </summary>
        public void RenderAll()
        {
            highestWater = lowestWater = waterManager.GetHeightAt(0, 0);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    RenderUnit(x, y);
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
                /*units[x, y].SetMaterial(typemap.GetTypeAt(x, y).material);
                units[x, y].EnableDecoration(true);
                units[x, y].SetHeight(landHeight);*/
                units[x, y].Set(
                    typemap.GetTypeAt(x, y).material,
                    true,
                    landHeight);
            }
            else
            {
                /*units[x, y].SetMaterial(water.material);
                units[x, y].EnableDecoration(false);
                units[x, y].SetHeight(waterHeight + (UnityEngine.Random.value - 0.5f) * visualWaterShake);*/
                units[x, y].Set(
                    water.material,
                    false,
                    waterHeight + (UnityEngine.Random.value - 0.5f) * visualWaterShake);

                if (waterHeight < lowestWater)
                    lowestWater = waterHeight;
                else if (waterHeight > highestWater)
                    highestWater = waterHeight;
            }
        }

        public void Terminate()
        {
            GameObject.FindGameObjectsWithTag("Unit").
                ToList().ForEach(x => DestroyImmediate(x));
        }
    }
}