using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Todo: remove this for like showInEditor bool with onvalidate type beat ??
    /// </summary>
    [CustomEditor(typeof(EndingSetup))]
    public class EndingSetupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EndingSetup endingSetup = target as EndingSetup;
            if (GUILayout.Button("Show Preview"))
                endingSetup.ShowPreview();

            if (GUILayout.Button("Hide Preview"))
                endingSetup.HidePreview();

        }
    }
}
