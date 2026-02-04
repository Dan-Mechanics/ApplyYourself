using UnityEngine;

namespace ApplyYourself
{
    public class PlaceholderMaterial : MonoBehaviour, IPlaceholder
    {
        [SerializeField] private Material[] materials = default;
        private MeshRenderer rend;

        private void SetMaterial(Material material)
        {
            if(rend == null)
                rend = GetComponent<MeshRenderer>();

            rend.sharedMaterial = material;
        }

        public void SetAs(Ending ending)
        {
            int index = Utils.GetIndexByName(materials, ending);
            SetMaterial(materials[index]);
        }
    }
}
