using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// If water cover it, disable decoration, dont destroy.
    /// </summary>
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

        public void EnableDecoration(bool value)
        {
            if (decoration != null)
                decoration.gameObject.SetActive(value);
        }

        public void SetHeight(float height)
        {
            Vector3 pos = transform.position;
            pos.y = height;
            transform.position = pos;   
        }

        public void SetMaterial(Material material) => renderer.material = material;
        public void SetDecoration(GameObject prefab)
        {
            if (decoration != null)
            {
                Object.Destroy(decoration.gameObject);
                decoration = null;
            }

            if (prefab == null)
                return;

            decoration = Object.Instantiate(prefab).transform;
            decoration.SetParent(transform);
            decoration.localPosition = Vector3.zero;
            decoration.localRotation = Quaternion.identity;
            decoration.name = prefab.name;
        }
    }
}