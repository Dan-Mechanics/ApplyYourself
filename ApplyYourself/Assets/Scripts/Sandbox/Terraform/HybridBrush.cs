using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(HybridBrush), menuName = nameof(HybridBrush))]
    public class HybridBrush : Brush
    {
        public EasyBinding invert;
        public UnitType type;

        public override void Apply(List<Vector2Int> positions)
        {
            float direction = invert.IsHeld ? -1f : 1f;
            float motion = direction * strength * Time.fixedDeltaTime;

            Decorate(positions, invert.IsHeld ? null : type);
            Raise(positions, motion);
        }
    }
}
