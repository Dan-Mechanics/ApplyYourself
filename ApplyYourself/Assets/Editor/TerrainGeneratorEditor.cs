using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Todo: remove this for like showInEditor bool with onvalidate type beat ??
    /// </summary>
    [CustomEditor(typeof(TerrainManager))]
    public class TerrainGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TerrainManager terrainManager = target as TerrainManager;
            if (GUILayout.Button("Show Preview"))
            {
                terrainManager.ClearChunks();
                terrainManager.Setup();
            }

            if (GUILayout.Button("Clear"))
                terrainManager.ClearChunks();

        }
    }
}
