using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class LandManager : MonoBehaviour
    {
        public float[,] Heightmap => heightmap;
        public UnitType[,] Typemap => typemap;
        
        public event Action<List<Vector2Int>> OnRaiseArea; 
        public event Action<List<Vector2Int>> OnDecorateArea; 

        [SerializeField] private int width = default;
        [SerializeField] private float minHeight = default;
        [SerializeField] private float maxHeight = default;
        [SerializeField] private EasyBinding removeLand = default;
        [SerializeField] private UnitType city = default;
        [SerializeField] private UnitType plains = default;

        private float[,] waterHeightmap;
        private UnitType[,] typemap;
        private float[,] heightmap;

        public void Initialize(IHeightmap heightmapStartup, ITypemap typemapStartup)
        {
            heightmap = heightmapStartup.GetBulk();
            typemap = typemapStartup.GetBulk();
        }

        public void Setup(float[,] waterHeightmap)
        {
            this.waterHeightmap = waterHeightmap;
        }
        
        private void Update()
        {
            if (removeLand.WasPressed)
                RemoveLand();
        }

        private void RemoveLand()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    heightmap[x, y] = minHeight;
                }
            }
        }

        public void RaiseArea(List<Vector2Int> positions, float meters)
        {
            for (int i = positions.Count - 1; i >= 0; i--)
            {
                Vector2Int pos = positions[i];
                if (typemap[pos.x, pos.y] == city)
                {
                    positions.RemoveAt(i);
                    continue;
                }

                heightmap[pos.x, pos.y] = Mathf.Clamp(heightmap[pos.x, pos.y] + meters, minHeight, maxHeight);
            }

            OnRaiseArea?.Invoke(positions);
        }

        public void DecorateArea(List<Vector2Int> positions, UnitType type)
        {
            if (type == null)
                type = plains;

            for (int i = positions.Count - 1; i >= 0; i--)
            {
                Vector2Int pos = positions[i];
                if (!CanChangeTypeAtPos(pos, type))
                {
                    positions.RemoveAt(i);
                    continue;
                }

                typemap[pos.x, pos.y] = type;
            }

            OnDecorateArea?.Invoke(positions);
        }

        private bool CanChangeTypeAtPos(Vector2Int pos, UnitType type)
        {
            Debug.Log(heightmap);
            Debug.Log(waterHeightmap);
            bool isLand = heightmap[pos.x, pos.y] >= waterHeightmap[pos.x, pos.y];
            bool validType = typemap[pos.x, pos.y] != type && typemap[pos.x, pos.y] != city;

            return isLand && validType;
        }
    }
}