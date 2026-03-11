using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class WaterManager : MonoBehaviour, IHeightmap
    {
        [SerializeField] private float minWaterHeight = default;
        [SerializeField] private float waterHeight = default;
        [SerializeField] private int width = default;
        [SerializeField] private int deadzone = default;
        [SerializeField] private float waterHeightPerTick = default;
        [SerializeField] private float raiseInterval = default;

        private IHeightmap landManager;
        private ITypemap typemap;
        private float[,] readBuffer;
        private float[,] writeBuffer;
        private float next;

        public void Initialize(IHeightmap landManager, ITypemap typemap)
        {
            this.landManager = landManager;
            this.typemap = typemap;

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

        public void RaiseArea(List<Vector2Int> positions, float meters, bool updateVisual)
        {
            if (meters == 0f || !updateVisual)
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
            float[,] landBulk = landManager.GetBulk();
            UnitType[,] typeBulk = typemap.GetBulk();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    float parentHeight = readBuffer[x, y];
                    Raise(x - 1, y, parentHeight, landBulk, typeBulk);
                    Raise(x + 1, y, parentHeight, landBulk, typeBulk);
                    Raise(x, y - 1, parentHeight, landBulk, typeBulk);
                    Raise(x, y + 1, parentHeight, landBulk, typeBulk);
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

        public float GetHeightAt(int x, int y) => readBuffer[x, y];
        public float[,] GetBulk() => readBuffer;
    }
}