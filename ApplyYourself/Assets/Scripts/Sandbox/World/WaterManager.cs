using UnityEngine;

namespace ApplyYourself
{
    public class WaterManager : MonoBehaviour
    {
        [SerializeField] private int width = default;
        [SerializeField] private float minWaterHeight = default;
        [SerializeField] private float maxWaterHeight = default;
        [SerializeField] private float interval = default;
        [SerializeField] private float visualShake = default;
        [SerializeField] private GridSpawner spawner = default;
        [SerializeField] private UnitType water = default;

        private LandManager typemap;
        private UnitVisual[,] unitVisuals;
        private float[,] heightBufferA;
        private float[,] heightBufferB;
        private bool swap;

        public void SetTypemap(LandManager typemap) => this.typemap = typemap;

        private void Start() => Initialize();

        public void Initialize()
        {
            GameObject[,] grid = spawner.SpawnGrid(width);
            heightBufferA = new float[width, width];
            heightBufferB = new float[width, width];
            unitVisuals = new UnitVisual[width, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Transform unit = grid[x, y].transform;
                    unitVisuals[x, y].Assign(unit, unit.GetChild(0).GetComponent<MeshRenderer>());
                }
            }

            SetHeight(0, width - 1, maxWaterHeight / 2f);
            UpdateAllHeights();
            UpdateAllTypes();

            InvokeRepeating(nameof(Tick), 0f, interval);
        }

        private void Tick()
        {
            Compute(swap ? heightBufferB : heightBufferA, swap ? heightBufferA : heightBufferB);
            swap = !swap;

           // UpdateAllHeights();
        }

        private void UpdateAllHeights()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Vector3 pos = unitVisuals[x, y].transform.position;
                    pos.y = heightBufferA[x, y] + Random.value * visualShake;
                    unitVisuals[x, y].transform.position = pos;
                }
            }
        }

        public float GetHeightAt(int x, int y) => heightBufferA[x, y];

        private void UpdateAllTypes()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    unitVisuals[x, y].SetAs(water);
                }
            }
        }

        private void SetHeight(int x, int y, float height)
        {
            heightBufferA[x, y] = height;
            heightBufferB[x, y] = height;
        }

        private void Compute(float[,] readBuffer, float[,] writeBuffer)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Raise(x, y + 1, readBuffer[x, y], writeBuffer);
                    Raise(x, y - 1, readBuffer[x, y], writeBuffer);
                    Raise(x - 1, y, readBuffer[x, y], writeBuffer);
                    Raise(x + 1, y, readBuffer[x, y], writeBuffer);
                }
            }
        }

        private void Raise(int x, int y, float height, float[,] writeBuffer)
        {
            if (x < 0 || y < 0 || x >= width || y >= width)
                return;

            if (height <= writeBuffer[x, y] || height <= typemap.GetHeightAt(x,y))
                return;

            writeBuffer[x, y] = Mathf.Clamp(writeBuffer[x, y] + height * 0.5f, minWaterHeight, maxWaterHeight);

            Vector3 pos = unitVisuals[x, y].transform.position;
            pos.y = writeBuffer[x, y] + Random.value * visualShake;
            unitVisuals[x, y].transform.position = pos;
        }
    }
}