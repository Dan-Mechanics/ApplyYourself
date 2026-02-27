using UnityEngine;

namespace ApplyYourself
{
    public class TextureTypemap : MonoBehaviour, ITypemap 
    {
        [SerializeField] private Texture2D typemap = default;
        [SerializeField] private Conversion[] conversions = default;

        public UnitType GetTypeAt(int x, int y)
        {
            x = Mathf.Clamp(x, 0, typemap.width - 1);
            y = Mathf.Clamp(y, 0, typemap.height - 1);
            Color color = typemap.GetPixel(x, y);

            for (int i = 0; i < conversions.Length; i++)
            {
                if (conversions[i].color == color)
                    return conversions[i].type;
            }

            return null;
        }

        [System.Serializable]
        private struct Conversion
        {
            public Color color;
            public UnitType type;
        }
    }
}