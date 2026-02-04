using UnityEngine;

namespace ApplyYourself
{
    // dont make this here !!!!
    public enum World { Placeholder, Water, Dry }
    
    public class FutureMaterial : MonoBehaviour, IPreviewable
    {
        [SerializeField] private World previewEnding = default;
        [SerializeField] private Material defaultMaterial = default;
        [SerializeField] private Material[] materials = default;
        private MeshRenderer rend;

        public void Setup(World ending) => SetMaterial(GetMaterial(ending));

        private Material GetMaterial(World ending)
        {
            if(ending == World.Placeholder)
                return defaultMaterial;

            int index = -1;
            for (int i = 0; i < materials.Length; i++)
            {
                string name = materials[i].name.ToLowerInvariant();
                if (name.Contains(ending.ToString().ToLowerInvariant()))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                Debug.LogError($"Ending material not found! --> {ending}");
                return null;
            }

            return materials[index];
        }

        private void SetMaterial(Material material)
        {
            if(rend == null)
                rend = GetComponent<MeshRenderer>();

            rend.sharedMaterial = material;
        }

        public void ShowPreview() => SetMaterial(GetMaterial(previewEnding));
        public void HidePreview() => SetMaterial(defaultMaterial);
        private void OnValidate() => HidePreview();
    }
}
