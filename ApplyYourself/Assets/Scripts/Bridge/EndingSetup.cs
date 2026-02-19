using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class EndingSetup : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabs = default;
        [SerializeField] private Ending previewEnding = default;

        private void Start()
        {
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].tag = "Untagged";
                Destroy(cameras[i]);
            }

            prefabs.ForEach(x => Spawn(x));

            // ===

            Algorithm algorithm = FindAnyObjectByType<Algorithm>();
            if (algorithm != null)
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

        private void Spawn(GameObject prefab)
        {
            Instantiate(prefab,
                prefab.transform.position, prefab.transform.rotation).name = prefab.name;
        }
        public void ShowPreview() => SetAs(previewEnding);
        public void HidePreview() => SetAs(Ending.Placeholder);
    }
}
