using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public abstract class Brush : ScriptableObject
    {
        public event Action<List<Vector2Int>, float, bool> OnRaise;
        public event Action<List<Vector2Int>, UnitType, bool> OnDecorate;

        public float size;
        public float strength;
        public Material previewMaterial;

        public abstract void Apply(List<Vector2Int> positions);

        protected void Raise(List<Vector2Int> positions, float motion, bool updateVisual)
        {
            OnRaise?.Invoke(positions, motion, updateVisual);
        }

        protected void Decorate(List<Vector2Int> positions, UnitType type, bool updateVisual)
        {
            OnDecorate?.Invoke(positions, type, updateVisual);
        }
    }
}
