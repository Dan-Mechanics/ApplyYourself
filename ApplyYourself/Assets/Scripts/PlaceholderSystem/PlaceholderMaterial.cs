using UnityEngine;

namespace ApplyYourself
{
    public class PlaceholderMaterial : BasePlaceholder
    {
        private MeshRenderer rend;

        private void SetMaterial(Material material)
        {
            if (rend == null)
                rend = GetComponent<MeshRenderer>();

            rend.sharedMaterial = material;
        }

        public override void SetAs(Ending ending)
        {
            ending = Utils.Filter(ending, endingOverrides);
            SetMaterial(Resources.Load<Material>($"{ending}/{resourceName}"));
        }
    }
}
