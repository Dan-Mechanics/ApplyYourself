using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(DecorationBrush), menuName = nameof(DecorationBrush))]
    public class DecorationBrush : Brush
    {
        public GameObject prefab;
        public string tag;
        // [Range(0f, 1f)] public float odds;

        public override void Apply(Collider[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                /*if (Random.value > odds)
                    continue;*/

                Transform parent = colliders[i].transform;
                parent.tag = tag;

                if (parent.childCount > 1)
                    Destroy(parent.GetChild(1).gameObject);

                if (prefab == null)
                    continue;

                Transform spawned = Instantiate(prefab).transform;
                spawned.parent = parent;
                spawned.localPosition = Vector3.zero;
                spawned.localRotation = Quaternion.identity;
            }
        }
    }
}
