using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class EndingSetup : MonoBehaviour
    {
        [SerializeField] private Ending previewEnding = default;

        private void Start()
        {
            SetAs(previewEnding);
            Destroy(gameObject);
        }

        private void SetAs(Ending ending)
        {
            Placeholder[] placeholders = FindObjectsByType<Placeholder>(FindObjectsSortMode.None);
            for (int i = 0; i < placeholders.Length; i++)
            {
                placeholders[i].SetAs(ending);
            }
        }

        public void ShowPreview() => SetAs(previewEnding);
        public void HidePreview() => SetAs(Ending.Placeholder);

        private void OnValidate()
        {
            if (previewEnding == Ending.Placeholder)
                previewEnding = Ending.Wet;
        }
    }
}
