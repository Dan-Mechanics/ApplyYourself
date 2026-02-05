using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = "ambient", menuName = "Ambient")]
    public class Ambient : ScriptableObject
    {
        [Min(0f)] public float density;
        public Color color = Color.gray;
    }
}
