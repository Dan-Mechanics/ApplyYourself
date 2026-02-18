using UnityEngine;

namespace ApplyYourself
{
    public class EndingSetup : MonoBehaviour
    {
        [SerializeField] private Ending previewEnding = default;

        private void Start()
        {
            Algorithm algorithm = FindAnyObjectByType<Algorithm>();
            if(algorithm != null)
            {
                SetAs(algorithm.ending);
                Destroy(algorithm.gameObject);
            }
            else
            {
                ShowPreview();
            }

            Destroy(gameObject);
        }

        public void SetAs(Ending ending)
        {
            BasePlaceholder[] placeholders = FindObjectsByType<BasePlaceholder>(FindObjectsSortMode.None);
            for (int i = 0; i < placeholders.Length; i++)
            {
                placeholders[i].SetAs(ending);
            }
        }

        public void ShowPreview() => SetAs(previewEnding);
        public void HidePreview() => SetAs(Ending.Placeholder);
    }
}
