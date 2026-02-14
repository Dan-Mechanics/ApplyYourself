using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(DecorationBrush), menuName = nameof(DecorationBrush))]
    public class DecorationBrush : Brush
    {
        public GameObject prefab;
        public LayerMask mask;

        public override void Apply(Vector3 point)
        {
            if (Random.value > 0.5f)
            {
                Collider[] colliders = Physics.OverlapSphere(point, size, mask, QueryTriggerInteraction.Ignore);
                for (int i = 0; i < colliders.Length; i++)
                {
                    Destroy(colliders[i].gameObject);
                }
            }

            point.x += Random.value * 10f;
            point.z += Random.value * 10f;

            // my idea is to use erm physics to basically hack this in,
            // and then when the game is done use more hacks.
            Instantiate(prefab, point, Quaternion.identity);
        }
    }
}
