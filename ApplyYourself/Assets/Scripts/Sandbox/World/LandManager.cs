using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class LandManager : MonoBehaviour, IHeightmap, ITypemap
    {
        public event Action<List<Vector2Int>> OnRender; 
        public event Action<List<Vector2Int>> OnRedecorate; 
        
        [SerializeField] private int width = default;
        [SerializeField] private float minHeight = default;
        [SerializeField] private float maxHeight = default;
        [SerializeField] private EasyBinding removeLand = default;
        [SerializeField] private UnitType city = default;
        [SerializeField] private UnitType plains = default;

        private IHeightmap waterManager;
        private UnitType[,] typemap;
        private float[,] heightmap;

        public void Initialize(IHeightmap heightmapStartup, ITypemap typemapStartup, IHeightmap waterManager)
        {
            this.waterManager = waterManager;
            heightmap = nheightmapStartup.GetBulk();
            typemap = typemapStartup.GetBulk();
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

            OnRender?.Invoke(positions);
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

            OnRedecorate?.Invoke(positions);
        }

        private bool CanChangeTypeAtPos(Vector2Int pos, UnitType type)
        {
            bool isLand = heightmap[pos.x, pos.y] >= waterManager.GetHeightAt(pos.x, pos.y);
            bool validType = typemap[pos.x, pos.y] != type && typemap[pos.x, pos.y] != city;
            return isLand && validType;
        }

        public float GetHeightAt(int x, int y) => heightmap[x, y];
        public UnitType GetTypeAt(int x, int y) => typemap[x, y];
        public float[,] GetBulk() => heightmap;
        UnitType[,] ITypemap.GetBulk() => typemap;
    }
}