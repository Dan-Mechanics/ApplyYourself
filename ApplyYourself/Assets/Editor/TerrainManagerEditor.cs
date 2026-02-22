using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    [CustomEditor(typeof(TerrainManager))]
    public class TerrainManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TerrainManager terrainManager = target as TerrainManager;
            if (GUILayout.Button("Show Preview"))
                terrainManager.SpawnGrid();

            if (GUILayout.Button("Clear"))
                terrainManager.ClearChunks();

        }
    }
}
