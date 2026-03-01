using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    [CustomEditor(typeof(UnitManager))]
    public class LandManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UnitManager landManager = target as UnitManager;
            if (GUILayout.Button("Show Preview"))
                landManager.InitializeDebug();

            if (GUILayout.Button("Clear"))
                landManager.Terminate();

        }
    }
}
