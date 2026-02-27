using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(UnitType), menuName = nameof(UnitType))]
    public class UnitType : ScriptableObject
    {
        public Material material;
        public GameObject decoration;

        // FIX NAME.
        [Range(0f, 1f)] public float waterPercentage;
    }
}
