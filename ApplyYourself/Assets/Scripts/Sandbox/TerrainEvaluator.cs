using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class TerrainEvaluator : MonoBehaviour
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private float interval = default;
        [SerializeField] private Pair flooded = default;
        [SerializeField] private List<Pair> pairs = default;

        private float[,] landHeightmap;
        private float[,] waterHeightmap;
        private UnitType[,] landTypemap;

        public void Setup(float[,] landHeightmap, float[,] waterHeightmap, UnitType[,] landTypemap)
        {
            this.landHeightmap = landHeightmap;
            this.waterHeightmap = waterHeightmap;
            this.landTypemap = landTypemap;

            pairs.ForEach(x => x.SetToDefault());
            InvokeRepeating(nameof(Display), interval, interval);
        }

        private void Display()
        {
            Ending ending = GetEnding();
            for (int i = 0; i < pairs.Count; i++)
            {
                Pair pair = pairs[i];
                pair.bar.fillAmount = (float)pair.count / pair.threshold;
            }

            flooded.bar.fillAmount = (float)flooded.count / flooded.threshold;
            icon.sprite = Resources.Load<Sprite>($"Icons/{ending.ToString().ToLowerInvariant()}");
        }

        private void Tick()
        {
            flooded.SetToDefault();
            pairs.ForEach(x => x.SetToDefault());

            int width = landHeightmap.GetLength(0);
            int height = landHeightmap.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    UnitType type = landTypemap[x, y];
                    if (landHeightmap[x, y] >= waterHeightmap[x, y])
                    {
                        pairs.Find(x => x.type == type)?.Increment();
                    }
                    else if (type == flooded.type)
                    {
                        flooded.Increment();
                    }
                }
            }
        }

        public Ending GetEnding()
        {
            Tick();
            bool isFlooded = flooded.count >= flooded.threshold;
            if (isFlooded)
                return flooded.ending;

            // SORT.
            pairs = pairs.OrderByDescending(x => x.count).ToList();
            for (int i = 0; i < pairs.Count; i++)
            {
                // IF IT FAILS, WE GO TO THE NEXT.
                Pair pair = pairs[i];
                if (pair.count >= pair.threshold)
                    return pair.ending;
            }

            // ELSE. 
            return Ending.FloatingCity;
        }

        [System.Serializable]
        private class Pair
        {
            public Image bar;
            public UnitType type;
            public Ending ending;
            public int threshold;
            [HideInInspector] public int count;

            public void Increment() => count++;
            public void SetToDefault() => count = 0;

            public void Log()
            {
                StringBuilder builder = new StringBuilder();
                char splitter = '_';
                builder.Append(ending).Append(splitter);
                builder.Append(count).Append(splitter);
                builder.Append(threshold).Append(splitter);
                print(builder.ToString());
            }
        }
    }
}
