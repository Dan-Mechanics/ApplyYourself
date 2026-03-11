using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class WaterManager : MonoBehaviour
    {
        public float[,] Heightmap => readBuffer;
        
        [SerializeField] private float minWaterHeight = default;
        [SerializeField] private float waterHeight = default;
        [SerializeField] private int width = default;
        [SerializeField] private int deadzone = default;
        [SerializeField] private float waterHeightPerTick = default;
        [SerializeField] private float raiseInterval = default;

        private float[,] landHeightmap;
        private UnitType[,] landTypemap;

        private float[,] readBuffer;
        private float[,] writeBuffer;
        private float next;

        public void Setup(float[,] landHeightmap, UnitType[,] landTypemap)
        {
            this.landHeightmap = landHeightmap;
            this.landTypemap = landTypemap;

            readBuffer = new float[width, width];
            writeBuffer = new float[width, width];
            SetHeight(0, width - 1, waterHeight);
        }

        private void FixedUpdate()
        {
            if (Time.time < next)
                return;

            RaiseWaterLevel();
            next = Time.time + raiseInterval;
        }

        private void SetHeight(int x, int y, float height)
        {
            writeBuffer[x, y] = height;
            readBuffer[x, y] = height;
        }

        public void RaiseArea(List<Vector2Int> positions, float meters)
        {
            if (meters == 0f)
                return;

            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;
                if (x >= deadzone || y <= width - deadzone - 1)
                    SetHeight(x, y, minWaterHeight);
            }
        }

        public void Tick()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    float parentHeight = readBuffer[x, y];
                    Raise(x - 1, y, parentHeight, landHeightmap, landTypemap);
                    Raise(x + 1, y, parentHeight, landHeightmap, landTypemap);
                    Raise(x, y - 1, parentHeight, landHeightmap, landTypemap);
                    Raise(x, y + 1, parentHeight, landHeightmap, landTypemap);
                }
            }

            // SWAP. ===
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    readBuffer[x, y] = writeBuffer[x, y];
                }
            }
        }

        private void RaiseWaterLevel()
        {
            waterHeight += waterHeightPerTick;
            SetHeight(0, width - 1, waterHeight);
        }

        private void Raise(int x, int y, float parentHeight, float[,] landBulk, UnitType[,] typeBulk)
        {
            if (x < 0 || y < 0 || x >= width || y >= width)
                return;

            float height = readBuffer[x, y];
            if (height >= parentHeight || landBulk[x, y] >= parentHeight)
                return;

            height += parentHeight * typeBulk[x, y].waterPercentage;
            writeBuffer[x, y] = Mathf.Clamp(height, minWaterHeight, parentHeight);
        }
    }
}