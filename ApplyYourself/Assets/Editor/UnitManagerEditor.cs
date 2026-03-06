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
                landManager.Initialize(FindAnyObjectByType<TextureHeightmap>(), FindAnyObjectByType<TextureTypemap>(), waterManager);
                waterManager.Initialize(landManager, landManager);
                unitManager.Initialize(landManager, landManager, waterManager);
                unitManager.RenderAll();
            }

            if (GUILayout.Button("Clear"))
                unitManager.Terminate();

        }
    }
}
