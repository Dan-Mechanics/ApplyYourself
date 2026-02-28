using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public abstract class Brush : ScriptableObject
    {
        public float size;
        public float strength;
        public Material previewMaterial;

        public virtual void Setup() { }
        public abstract void Apply(List<Vector2Int> positions);
    }
}
