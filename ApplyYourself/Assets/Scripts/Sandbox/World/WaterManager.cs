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

        [SerializeField] private LandManager landManager = default;
        private float[,] waterHeights;
        private bool[,] hasChanged;
        private bool done;

       // private void Start() => Initialize();

        public void Initialize()
        {
            waterHeights = new float[width, width];
            hasChanged = new bool[width, width];
            //Tick();

            waterHeights[0, width - 1] = maxWaterHeight / 2f;
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
                waterHeights[x, y] = minWaterHeight;
            }
        }

        public void Tick()
        {
            if (done && !Input.GetKey(KeyCode.Space))
                return;
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
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

            done = true;
        }

        private void Raise(int x, int y, float parentHeight)
        {
            if (x < 0 || y < 0 || x >= width || y >= width)
                return;

            if (hasChanged[x, y])
                return;

            // make is so that if there is terrain there, make it zero
            float height = waterHeights[x, y];
            if (height <= landManager.GetHeightAt(x, y))
            {
                waterHeights[x, y] = minWaterHeight;
                return;
            }

            if (height < parentHeight)
            {
                height = Mathf.Clamp(minWaterHeight, maxWaterHeight, height + parentHeight * landManager.GetTypeAt(x, y).waterPercentage);
                if (height == waterHeights[x, y])
                    return;

                hasChanged[x, y] = true;
                waterHeights[x, y] = height;
            }
        }
    }
}