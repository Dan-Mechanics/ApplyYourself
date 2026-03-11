using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(RaiseBrush), menuName = nameof(RaiseBrush))]
    public class RaiseBrush : Brush
    {
        public EasyBinding invert;

        public override void Apply(List<Vector2Int> positions)
        {
            float direction = invert.IsHeld ? -1f : 1f;
            float motion = direction * strength * Time.fixedDeltaTime;
            Raise(positions, motion, true);
        }
    }
}
