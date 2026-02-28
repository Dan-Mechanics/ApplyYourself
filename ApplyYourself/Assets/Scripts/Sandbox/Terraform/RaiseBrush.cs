using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(RaiseBrush), menuName = nameof(RaiseBrush))]
    public class RaiseBrush : Brush
    {
        public event Action<List<Vector2Int>, float> OnRaise;
        public EasyBinding invert;

        public override void Apply(List<Vector2Int> positions)
        {
            float direction = invert.IsHeld ? -1f : 1f;
            float motion = direction * strength * Time.fixedDeltaTime;
            OnRaise?.Invoke(positions, motion);
        }
    }
}
