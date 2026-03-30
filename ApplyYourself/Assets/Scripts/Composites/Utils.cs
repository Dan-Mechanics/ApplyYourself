using System;
using UnityEngine;

namespace ApplyYourself
{
    public static class Utils
    {
        public static T StringToEnum<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
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