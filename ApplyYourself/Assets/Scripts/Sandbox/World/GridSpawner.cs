using UnityEngine;

namespace ApplyYourself
{
    [System.Serializable]
    public class GridSpawner
    {
        public GameObject prefab;
        public float spacing;

        public GameObject[,] SpawnGrid(int width)
        {
            GameObject[,] grid = new GameObject[width, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Vector3 position = new Vector3(x * spacing, 0f, y * spacing);
                    position.x += 0.5f * spacing;
                    position.z += 0.5f * spacing;
                        
                    GameObject go = Object.Instantiate(prefab, position, Quaternion.identity);
                    go.name = prefab.name;
                    grid[x, y] = go;
                }
            }

            return grid;    
        }
    }
}