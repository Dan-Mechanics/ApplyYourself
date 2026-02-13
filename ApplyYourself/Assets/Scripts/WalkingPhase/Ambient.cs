using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(Ambient), menuName = nameof(Ambient))]
    public class Ambient : ScriptableObject
    {
        [Min(0f)] public float density;
        public Color color = Color.gray;
    }
}
