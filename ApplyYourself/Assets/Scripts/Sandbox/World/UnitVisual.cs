using UnityEngine;

namespace ApplyYourself
{
    public struct UnitVisual
    {
        public Transform transform;
        public MeshRenderer renderer;
        public Transform decoration;

        public void Assign(Transform transform, MeshRenderer renderer)
        {
            this.transform = transform;
            this.renderer = renderer;
        }

        public void SetAs(UnitType type)
        {
            renderer.material = type.material;
            if (decoration != null)
            {
                Object.Destroy(decoration.gameObject);
                decoration = null;
            }

            if (type.decoration == null)
                return;

            decoration = Object.Instantiate(type.decoration).transform;
            decoration.SetParent(transform);
            decoration.localPosition = Vector3.zero;
            decoration.localRotation = Quaternion.identity;
            decoration.name = type.decoration.name;
        }
    }
}