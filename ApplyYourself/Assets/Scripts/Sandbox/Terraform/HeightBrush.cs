using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(HeightBrush), menuName = nameof(HeightBrush))]
    public class HeightBrush : Brush
    {
        public override void Apply(Collider[] colliders)
        {
            float direction = Input.GetKey(KeyCode.LeftShift) ? -1f : 1f;
            Vector3 displacement = direction * strength * Time.fixedDeltaTime * Vector3.up;
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].transform.position += displacement;
            }
        }
    }
}
