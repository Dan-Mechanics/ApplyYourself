using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Possible: add to repo.
    /// </summary>
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
    }
}