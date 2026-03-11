using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(DecorateBrush), menuName = nameof(DecorateBrush))]
    public class DecorateBrush : Brush
    {
        public EasyBinding invert;
        public UnitType type;

        public override void Apply(List<Vector2Int> positions)
        {
            // IDEA: DO SOMETHING WITH STRENGTH HERE.
            Decorate(positions, invert.IsHeld ? null : type, true);
        }
    }
}
