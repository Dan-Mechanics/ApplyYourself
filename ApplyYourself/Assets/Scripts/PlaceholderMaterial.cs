using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// todo, add clever overrides.
    /// </summary>
    public class PlaceholderMaterial : Placeholder
    {
        private MeshRenderer rend;

        private void SetMaterial(Material material)
        {
            if(rend == null)
                rend = GetComponent<MeshRenderer>();

            rend.sharedMaterial = material;
        }

        public override void SetAs(Ending ending)
        {
            Material mat = Resources.Load<Material>($"{ending}/{resourceName}");
            SetMaterial(mat);
        }
    }
}
