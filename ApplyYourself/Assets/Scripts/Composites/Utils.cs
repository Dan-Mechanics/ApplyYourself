using System;
using UnityEngine;

namespace ApplyYourself
{
    public static class Utils
    {
        public static Ending Filter(Ending ending, EndingOverride[] endingOverrides) 
        {
            for (int i = 0;i < endingOverrides.Length; i++)
            {
                if (endingOverrides[i].from == ending)
                    return endingOverrides[i].to;
            }

            return ending;
        }

        public static T StringToEnum<T>(string str)
        {
            return (T)System.Enum.Parse(typeof(T), str);
        }

        public static bool IsStringValid(string str)
        {
            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
        }

        public static Vector3 Add(Vector3 a, Vector2 b)
        {
            a.x += b.x;
            a.y += b.y;
            return a;
        }
    }
}