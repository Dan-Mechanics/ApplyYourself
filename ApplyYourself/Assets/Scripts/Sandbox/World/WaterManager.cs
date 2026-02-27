using UnityEngine;

namespace ApplyYourself
{
    public class WaterManager : MonoBehaviour
    {
        [SerializeField] private int width = default;
        [SerializeField] private float minWaterHeight = default;
        [SerializeField] private float maxWaterHeight = default;
        [SerializeField] private float raise = default;
        [SerializeField] private GridSpawner spawner = default;
        [SerializeField] private UnitType water = default;

        private UnitVisual[,] unitVisuals;
        private float[,] heightBufferA;
        private float[,] heightBufferB;
        private bool swap;

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

            heightBufferA[0, 0] = maxWaterHeight;
            UpdateAllHeights();
            UpdateAllTypes();
        }

        private void UpdateAllHeights()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Vector3 pos = unitVisuals[x, y].transform.position;
                    pos.y = heightBufferA[x, y];
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

        private void FixedUpdate()
        {
            Tick(swap ? heightBufferB : heightBufferA, swap ? heightBufferA : heightBufferB);
            swap = !swap;

            UpdateAllHeights();
        }

        private void Tick(float[,] readBuffer, float[,] writeBuffer)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    RaiseNeighbours(x, y, readBuffer, writeBuffer);
                }
            }
        }

        private void RaiseNeighbours(int x, int y, float[,] readBuffer, float[,] writeBuffer)
        {
            float motion = readBuffer[x, y] * raise * Time.fixedDeltaTime;
            
            // get a radnom point around diag plus cardiinal and then move it towards it.
            Raise(x + 1, y, motion, writeBuffer);
            Raise(x - 1, y, motion, writeBuffer);
            Raise(x, y + 1, motion, writeBuffer);
            Raise(x, y - 1, motion, writeBuffer);

            Raise(x - 1, y + 1, motion, writeBuffer);

            Raise(x + 1, y + 1, motion, writeBuffer);

            Raise(x  -1, y - 1, motion, writeBuffer);
            Raise(x + 1, y - 1, motion, writeBuffer);
        }

        private void Raise(int x, int y, float motion, float[,] writerBuffer)
        {
            if (x >= 0 && y >= 0 && x < width && y < width)
                writerBuffer[x, y] = Mathf.Clamp(writerBuffer[x, y] + motion, minWaterHeight, maxWaterHeight);
        }
    }
}