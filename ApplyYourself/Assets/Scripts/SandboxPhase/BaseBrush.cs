using UnityEngine;

namespace ApplyYourself
{
    public abstract class BaseBrush : ScriptableObject
    {
        public float size;
        public float strength;
        public Material previewMaterial;

        public virtual void Setup() { }
        public abstract void Apply(Collider[] colliders);
    }
}
