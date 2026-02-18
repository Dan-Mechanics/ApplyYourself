using UnityEngine;

namespace ApplyYourself
{
    public static class Utils
    {
        public static int GetIndexByName<T>(T[] array, Ending ending) where T : Object
        {
            string str = ending.ToString().ToLowerInvariant();
            string a = "_" + str;
            string b = str + "_"; 
            
            for (int i = 0; i < array.Length; i++)
            {
                string name = array[i].name.ToLowerInvariant();
                if (name.Contains(a) || name.Contains(b))
                    return i;
            }

            Debug.LogWarning($"Could not find element in array --> {str}.");
            return -1;
        }
        
        public static Vector3 Flatten(Vector3 vector)
        {
            vector.y = 0f;
            return vector;
        }

        /// <summary>
        /// Keep scale in mind !!
        /// </summary>
        public static bool TryGetIndexFromPos(int x, int z, int maxX, int maxZ, out int index)
        {
            index = 0;

            if (x < 0 || x > maxX)
                return false;

            if (z < 0 || z > maxZ)
                return false;

            index = x + z * (maxX + 1);
            return true;
        }

        public static Vector3Int GetCellPos(Vector3 worldPos, float size)
        {
            return new Vector3Int(
                Mathf.FloorToInt(worldPos.x / size),
                Mathf.FloorToInt(worldPos.y / size),
                Mathf.FloorToInt(worldPos.z / size));
        }

        public static Vector2Int GetCellPos(Vector2 worldPos, float size)
        {
            return new Vector2Int(
                Mathf.FloorToInt(worldPos.x / size),
                Mathf.FloorToInt(worldPos.y / size));
        }

        public static Ending Filter(Ending ending, EndingOverride[] endingOverrides) 
        {
            for (int i = 0;i < endingOverrides.Length; i++)
            {
                if (endingOverrides[i].from == ending)
                    return endingOverrides[i].to;
            }

            return ending;
        }
    }
}