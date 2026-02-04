using UnityEngine;

namespace ApplyYourself
{
    // dont make this here !!!!
    public enum Ending { Water, Dry }
    
    public class FutureMaterial : MonoBehaviour, IPreviewable
    {
        [SerializeField] private Ending previewEnding = default;
        [SerializeField] private Material defaultMaterial = default;
        [SerializeField] private Material[] materials = default;
        private MeshRenderer rend;

        public void Setup(Ending ending) => SetMaterial(GetMaterial(ending));

        private Material GetMaterial(Ending ending)
        {
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
