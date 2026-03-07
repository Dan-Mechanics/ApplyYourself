using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class PerlinWater : MonoBehaviour, IHeightmap
    {
        [SerializeField] private int width = default;
        [SerializeField] private float amplitude = default;
        [SerializeField] private float verticalShift = default;
        [SerializeField] private float period = default;
        
        public float GetHeightAt(int x, int y)
        {
            // float value = Vector2.Distance(Vector2.zero, new Vector2(x,y)) + Time.time;
            //float value = x%y + Time.time;
            // return Mathf.Sin(value * period) * amplitude + verticalShift;
            //float scaler = 0.15f;
          //  ..ar[x, y] = Mathf.PerlinNoise(x * scaler, y * scaler);
            return Mathf.PerlinNoise(x* period + Time.time*0.2f, y* period + Time.time*0.2f) * amplitude + verticalShift;
        }

        public float[,] GetBulk()
        {
            float[,] amplitude = new float[width, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    amplitude[x, y] = GetHeightAt(x, y);
                }
            }

            return amplitude;
        }
    }
}