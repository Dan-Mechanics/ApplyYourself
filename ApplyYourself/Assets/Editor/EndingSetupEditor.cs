using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ApplyYourself
{
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
