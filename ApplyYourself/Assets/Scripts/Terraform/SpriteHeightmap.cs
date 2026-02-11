using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class SpriteHeightmap : MonoBehaviour, IHeightmap
    {
        [SerializeField] private Sprite sprite = default;

        private float[,] orSoemthing;

        public void Setup()
        {
            // make orsomethng float[,] ...


        }

        public float GetHeight(float xPercentage, float yPercentage)
        {
            throw new System.NotImplementedException();
        }
    }
}