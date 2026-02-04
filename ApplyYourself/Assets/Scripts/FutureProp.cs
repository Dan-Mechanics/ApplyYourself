using UnityEngine;

namespace ApplyYourself
{
    public class FutureProp : MonoBehaviour, IPreviewable
    {
        [SerializeField] private Ending previewEnding = default;
        [SerializeField] private GameObject defaultPrefab = default;
        [SerializeField] private GameObject[] prefabs = default;

        public void Setup(Ending ending) => SetPrefab(GetPrefab(ending));

        /// <summary>
        ///  Make this general method in utils and put enum there too.
        /// </summary>
        /// <param name="ending"></param>
        /// <returns></returns>
        private GameObject GetPrefab(Ending ending)
        {
            int index = -1;
            for (int i = 0; i < prefabs.Length; i++)
            {
                string name = prefabs[i].name.ToLowerInvariant();
                if (name.Contains(ending.ToString().ToLowerInvariant()))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                Debug.LogError($"Ending prefab not found! --> {ending}");
                return null;
            }

            return prefabs[index];
        }

        private void SetPrefab(GameObject prefab)
        {
            string previewName = "preview";
            Transform preview = transform.Find(previewName);
            if (preview != null)
                preview.gameObject.SetActive(false);

            GameObject go = Instantiate(prefab);
            go.transform.SetParent(transform);
            go.transform.SetLocalPositionAndRotation(prefab.transform.localPosition,
                prefab.transform.localRotation);
            go.transform.localScale = prefab.transform.localScale;
            go.name = previewName;
        }

        public void ShowPreview() => SetPrefab(GetPrefab(previewEnding));
        public void HidePreview() => SetPrefab(defaultPrefab);
        private void OnValidate() => HidePreview();
    }
}
