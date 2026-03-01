using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// You could make difference SOLIDS with this.
    /// </summary>
    public class WaterManager : MonoBehaviour
    {
        [SerializeField] private LandManager landManager = default;
        [SerializeField] private float minWaterHeight = default;
        [SerializeField] private float waterHeight = default;
        // [SerializeField] private float maxWaterHeight = default;
        [SerializeField] private int width = default;
        [SerializeField] private int deadzone = default;

        private float[,] waterHeights;
        private bool[,] hasChanged;

        public void Initialize()
        {
            waterHeights = new float[width, width];
            hasChanged = new bool[width, width];
            waterHeights[0, width - 1] = waterHeight;
        }

        public void InitializeDebug()
        {
            Initialize();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    waterHeights[x, y] = waterHeight;
                }
            }
        }

        public float GetHeightAt(int x, int y) => waterHeights[x, y];

        public void RaiseArea(List<Vector2Int> positions, float motion)
        {
            if (motion == 0f)
                return;

            for (int i = 0; i < positions.Count; i++)
            {
                int x = positions[i].x;
                int y = positions[i].y;
                if (x >= deadzone || y <= width - deadzone - 1)
                    waterHeights[x, y] = minWaterHeight;
            }
        }

        public void Tick()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (hasChanged[x, y])
                        continue;

                    float parentHeight = waterHeights[x, y];
                    Raise(x - 1, y, parentHeight);
                    Raise(x + 1, y, parentHeight);
                    Raise(x, y - 1, parentHeight);
                    Raise(x, y + 1, parentHeight);
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    hasChanged[x, y] = false;
                }
            }
        }

        private void Raise(int x, int y, float parentHeight)
        {
            if (x < 0 || y < 0 || x >= width || y >= width)
                return;

            float height = waterHeights[x, y];
            if (height >= parentHeight || landManager.GetHeightAt(x, y) >= parentHeight)
                return;

            height = Mathf.Clamp(
                height + parentHeight * landManager.GetTypeAt(x, y).waterPercentage, minWaterHeight,
                parentHeight); 
                //Mathf.Min(parentHeight, maxWaterHeight)); 

            if (height == waterHeights[x, y])
                return;

            hasChanged[x, y] = true;
            waterHeights[x, y] = height;
        }
    }
}