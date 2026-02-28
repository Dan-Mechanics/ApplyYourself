using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// You could make difference SOLIDS with this.
    /// </summary>
    public class WaterManager : MonoBehaviour
    {
        [SerializeField] private float minWaterHeight = default;
        [SerializeField] private float maxWaterHeight = default;
        [SerializeField] private float waterHeight = default;
        [SerializeField] private float shake = default;
        [SerializeField] private int width = default;
        [SerializeField, Range(0f, 1f)] private float odds = default;
        [SerializeField] private int deadzone = default;

        [SerializeField] private LandManager landManager = default;
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

        public float GetHeightAt(int x, int y) => waterHeights[x, y] + Random.value * shake;
        //public float GetHeightAt(int x, int y) => waterHeights[x, y];

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

            /*if (Random.value > odds || hasChanged[x,y])
                return;*/

            // YOU COULD ALSO MAKE IT SO THAT
            // THIS IS ON THE THING ITSELF, LESS RANDOMN CALLS.
           /* if (Random.value > odds)
                return;*/

            float height = waterHeights[x, y];
            if (height < parentHeight && landManager.GetHeightAt(x, y) < parentHeight)
            {
                 height = Mathf.Clamp(height + parentHeight * landManager.GetTypeAt(x, y).waterPercentage, minWaterHeight, parentHeight);
                //height = Mathf.Clamp(minWaterHeight, maxWaterHeight, height + 0.1f);
                if (height != waterHeights[x, y])
                {
                    hasChanged[x, y] = true;
                    waterHeights[x, y] = height;
                }
            }
        }
    }
}