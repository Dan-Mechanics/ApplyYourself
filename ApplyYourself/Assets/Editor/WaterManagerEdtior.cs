using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    [CustomEditor(typeof(WaterManager))]
    public class WaterManagerEdtior : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            WaterManager waterManager = target as WaterManager;
            if (GUILayout.Button("Show Preview"))
                waterManager.Setup();

            if (GUILayout.Button("Clear"))
                waterManager.ClearChunks();

        }
    }
}
