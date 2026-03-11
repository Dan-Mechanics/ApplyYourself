using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class Algorithm : MonoBehaviour
    {
        [SerializeField] private int threshold = default;
        [SerializeField, Range(0f, 1f)] private float floodedThreshold = default;
        [SerializeField] private Image floodedBar = default;
        [SerializeField] private Image icon = default;
        [SerializeField] private float interval = default;
        [SerializeField] private UnitType city = default;
        [SerializeField] private List<Pair> pairs = default;

        private float floodedPercentage;
        private float[,] landHeightmap;
        private float[,] waterHeightmap;
        private UnitType[,] landTypemap;

        public void Setup(float[,] landHeightmap, float[,] waterHeightmap, UnitType[,] landTypemap)
        {
            this.landHeightmap = landHeightmap;
            this.waterHeightmap = waterHeightmap;
            this.landTypemap = landTypemap;
            InvokeRepeating(nameof(Display), interval, interval);
        }

        private void Display()
        {
            Ending ending = GetEnding();
            print(ending);

            for (int i = 0; i < pairs.Count; i++)
            {
                Pair pair = pairs[i];
                pair.bar.fillAmount = (float)pair.count / threshold;
            }

            floodedBar.fillAmount = floodedPercentage / floodedThreshold;
            icon.sprite = Resources.Load<Sprite>($"{ending}/icon");
        }

        private void Tick()
        {
            pairs.ForEach(x => x.SetToDefault());
            int totalCityCount = 0;
            int floodedCityCount = 0;

            int width = landHeightmap.GetLength(0);
            int height = landHeightmap.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    UnitType type = landTypemap[x, y];
                    if(type == city)
                        totalCityCount++;

                    if (landHeightmap[x, y] >= waterHeightmap[x, y])
                    {
                        FindAndIncrement(type);
                    }
                    else if (type == city)
                    {
                        floodedCityCount++;
                    }
                }
            }

            if (totalCityCount <= 0)
                totalCityCount = 1;

            floodedPercentage = (float)floodedCityCount / totalCityCount;
            Debug.Log(floodedPercentage);

        }

        private void FindAndIncrement(UnitType type)
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                if (pairs[i].type != type)
                    continue;

                pairs[i].count++;
                return;
            }
        }

        public Ending GetEnding()
        {
            Tick();
            bool isFlooded = floodedPercentage > floodedThreshold;
            if (isFlooded)
                return Ending.UnderwaterCity;

            Pair pair = pairs.OrderBy(x => x.count).First();
            if (pair.count >= threshold)
                return pair.ending;

            // ELSE. 
            return Ending.FloatingCity;
        }

        [System.Serializable]
        private class Pair
        {
            public Image bar;
            public UnitType type;
            public Ending ending;
            public int count;

            public void SetToDefault() => count = 0;
        }
    }
}
