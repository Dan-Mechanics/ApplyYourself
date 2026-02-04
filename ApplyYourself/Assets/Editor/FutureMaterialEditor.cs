using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    [CustomEditor(typeof(FutureMaterial))]
    public class FutureMaterialEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Show Preview"))
                GetPreviewables().ForEach(x => x.ShowPreview());

            if (GUILayout.Button("Hide Preview"))
                GetPreviewables().ForEach(x => x.HidePreview());

            base.OnInspectorGUI();  
        }

        private List<IPreviewable> GetPreviewables()
        {
            List<IPreviewable> result = new List<IPreviewable>();

            MonoBehaviour[] monos = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            foreach (MonoBehaviour mono in monos)
            {
                if (mono is IPreviewable previewable)
                    result.Add(previewable);
            }

            return result;
        }
    }
}
