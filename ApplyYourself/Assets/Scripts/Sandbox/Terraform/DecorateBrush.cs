using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(DecorateBrush), menuName = nameof(DecorateBrush))]
    public class DecorateBrush : Brush
    {
        public event Action<List<Vector2Int>, UnitType> OnDecorate;

        public EasyBinding invert;
        public UnitType type;

        public override void Apply(List<Vector2Int> positions)
        {
            // YOU CAN DO SOME CHANGES TO POSITIONS TO HAVE DIFFERENT EFFECTS.
            // LIKE ODDS WITH STRENGTH.
            OnDecorate?.Invoke(positions, invert.IsHeld ? null : type);
        }
    }
}
