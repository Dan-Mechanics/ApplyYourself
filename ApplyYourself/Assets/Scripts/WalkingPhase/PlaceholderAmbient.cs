using UnityEngine;

namespace ApplyYourself
{
    public class PlaceholderProp : Placeholder
    {
        private Camera cam;
        
        private void SetAmbient(Ambient amb)
        {
            if (cam == null)
                cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

            RenderSettings.fogDensity = amb.density;
            RenderSettings.fogColor = amb.color;

            bool fog = amb.density > 0f;

            cam.clearFlags = fog ? CameraClearFlags.SolidColor : CameraClearFlags.Skybox;
            cam.backgroundColor = amb.color;
            RenderSettings.fog = fog;
        }

        public override void SetAs(Ending ending)
        {
            ending = Utils.Filter(ending, endingOverrides);
            SetAmbient(Resources.Load<Ambient>($"{ending}/{resourceName}"));
        }
    }
}
