using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public abstract class Brush : ScriptableObject
    {
        public event Action<List<Vector2Int>, float> OnRaise;
        public event Action<List<Vector2Int>, UnitType> OnDecorate;

        public float size;
        public float strength;
        public Material previewMaterial;

        public abstract void Apply(List<Vector2Int> positions);

        protected void Raise(List<Vector2Int> positions, float motion)
        {
            OnRaise?.Invoke(positions, motion);
        }

        protected void Decorate(List<Vector2Int> positions, UnitType type)
        {
            OnDecorate?.Invoke(positions, type);
        }
    }
}
