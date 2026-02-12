using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Todo: remove this for like showInEditor bool with onvalidate type beat ??
    /// </summary>
    [CustomEditor(typeof(TerrainGenerator))]
    public class TerrainGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TerrainGenerator generator = target as TerrainGenerator;
            if (GUILayout.Button("Show Preview"))
                generator.Setup();

            if (GUILayout.Button("Clear"))
                generator.Clear();

        }
    }
}
