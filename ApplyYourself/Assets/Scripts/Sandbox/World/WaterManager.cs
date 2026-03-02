using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class WaterManager : MonoBehaviour
    {
        [SerializeField] private UnitManager unitManager = default;
        [SerializeField] private float minWaterHeight = default;
        [SerializeField] private float waterHeight = default;
        [SerializeField] private int width = default;
        [SerializeField] private int deadzone = default;

        private float[,] bufferA;
        private float[,] bufferB;
        private bool swap;

        public void Initialize()
        {
            bufferA = new float[width, width];
            bufferB = new float[width, width];
            SetHeight(0, width - 1, waterHeight);
        }

        public void InitializeDebug()
        {
            Initialize();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    SetHeight(x, y, waterHeight);
                }
            }
        }

        public float GetHeightAt(int x, int y) => (swap ? bufferB : bufferA)[x, y];
        private void SetHeight(int x, int y, float height) => bufferA[x, y] = bufferB[x, y] = height;

        public void RaiseArea(List<Vector2Int> positions, float motion)
        {
            if (motion == 0f)
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
            ComputeWater(swap ? bufferB : bufferA, swap ? bufferA : bufferB);
            swap = !swap;
        }

        private void ComputeWater(float[,] readBuffer, float[,] writeBuffer)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    float parentHeight = readBuffer[x, y];
                    Raise(x - 1, y, parentHeight, readBuffer, writeBuffer);
                    Raise(x + 1, y, parentHeight, readBuffer, writeBuffer);
                    Raise(x, y - 1, parentHeight, readBuffer, writeBuffer);
                    Raise(x, y + 1, parentHeight, readBuffer, writeBuffer);
                }
            }
        }

        private void Raise(int x, int y, float parentHeight, float[,] readBuffer, float[,] writeBuffer)
        {
            if (x < 0 || y < 0 || x >= width || y >= width)
                return;

            float height = readBuffer[x, y];
            if (height >= parentHeight || unitManager.GetHeightAt(x, y) >= parentHeight)
                return;

            height += parentHeight * unitManager.GetTypeAt(x, y).waterPercentage;
            writeBuffer[x, y] = Mathf.Clamp(height, minWaterHeight, parentHeight);
        }
    }
}