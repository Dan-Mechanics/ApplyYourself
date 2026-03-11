using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    [CustomEditor(typeof(UnitManager))]
    public class UnitManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UnitManager unitManager = target as UnitManager;
            if (GUILayout.Button("Show Preview"))
            {
                LandManager landManager = FindAnyObjectByType<LandManager>();
                WaterManager waterManager = FindAnyObjectByType<WaterManager>();
                waterManager.Initialize();
                landManager.Initialize(FindAnyObjectByType<TextureHeightmap>(), FindAnyObjectByType<TextureTypemap>());

                waterManager.Setup(landManager.Heightmap, landManager.Typemap);
                landManager.Setup(waterManager.Heightmap);

                unitManager.Setup(landManager.Typemap, landManager.Heightmap, waterManager.Heightmap);
                unitManager.RenderAll();
            }

            if (GUILayout.Button("Clear"))
                unitManager.Terminate();

        }
    }
}
