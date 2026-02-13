using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(DecorationBrush), menuName = nameof(DecorationBrush))]
    public class DecorationBrush : Brush
    {
        public GameObject prefab;

        public override void Apply(Vector3 point)
        {
            // my idea is to use erm physics to basically hack this in,
            // and then when the game is done use more hacks.
            Instantiate(prefab, point, Quaternion.identity);
        }
    }
}
