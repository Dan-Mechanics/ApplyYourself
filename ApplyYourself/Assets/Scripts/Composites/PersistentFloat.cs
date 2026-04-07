using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(PersistentFloat), menuName = nameof(PersistentFloat))]
    public class PersistentFloat : ScriptableObject
    {
        public float value;
    }
}
