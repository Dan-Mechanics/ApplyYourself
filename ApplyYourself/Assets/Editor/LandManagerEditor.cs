using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    [CustomEditor(typeof(LandManager))]
    public class LandManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LandManager landManager = target as LandManager;
            if (GUILayout.Button("Show Preview"))
                landManager.Initialize();

            if (GUILayout.Button("Clear"))
                landManager.Terminate();

        }
    }
}
