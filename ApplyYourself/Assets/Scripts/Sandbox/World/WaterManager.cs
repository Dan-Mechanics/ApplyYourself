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
        [SerializeField] private float interval = default;

        private IHeightmap landManager;
        private ITypemap typemap;
        private float[,] readBuffer;
        private float[,] writeBuffer;

        public void Initialize(IHeightmap landManager, ITypemap typemap)
        {
            this.landManager = landManager;
            this.typemap = typemap;

            readBuffer = new float[width, width];
            writeBuffer = new float[width, width];
            SetHeight(0, width - 1, waterHeight);
            InvokeRepeating(nameof(RaiseWaterLevel), interval, interval);
        }

        public float GetHeightAt(int x, int y) => readBuffer[x, y];

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
                    Raise(x - 1, y, parentHeight);
                    Raise(x + 1, y, parentHeight);
                    Raise(x, y - 1, parentHeight);
                    Raise(x, y + 1, parentHeight);
                }
            }

            // SWAP.
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

        private void Raise(int x, int y, float parentHeight)
        {
            if (x < 0 || y < 0 || x >= width || y >= width)
                return;

            float height = readBuffer[x, y];
            if (height >= parentHeight || landManager.GetHeightAt(x, y) >= parentHeight)
                return;

            height += parentHeight * typemap.GetTypeAt(x, y).waterPercentage;
            writeBuffer[x, y] = Mathf.Clamp(height, minWaterHeight, parentHeight);
        }
    }
}