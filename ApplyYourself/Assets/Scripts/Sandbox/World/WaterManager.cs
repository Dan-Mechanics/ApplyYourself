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

       // private void Start() => Initialize();

        public void Initialize()
        {
            waterHeights = new float[width, width];
            hasChanged = new bool[width, width];

            //waterHeights[0, width - 1] = maxWaterHeight / 2f;
            //hasChangedGrid[0, width - 1] = true;
        }

        public float GetHeightAt(int x, int y) => waterHeights[x, y];

        public void RaiseArea(List<Vector2Int> positions, float motion)
        {
            if (motion <= 0f)
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
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    /*if (hasChanged[x, y])
                    {
                        hasChanged[x, y] = false;
                        continue;
                    }*/

                    waterHeights[x, y] = waterHeight + Random.value * shake;
                   // hasChanged[x, y] = true;
                }
            }
        }
    }
}