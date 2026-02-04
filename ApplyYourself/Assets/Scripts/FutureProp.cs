using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Possible:
    /// make noita
    /// make terrain editor like level
    /// make terrain editor marhcing cubes
    /// easy peasy type beat.
    /// </summary>
    public class FutureProp : MonoBehaviour, IPreviewable
    {
        [SerializeField] private World previewEnding = default;
        public void Setup(World ending) => SetEnding(ending);

        private void SetEnding(World ending)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            Debug.Log(ending);
            transform.Find(ending.ToString().ToLowerInvariant()).gameObject.SetActive(true);   
        }

        public void ShowPreview() => SetEnding(previewEnding);
        public void HidePreview() => SetEnding(World.Placeholder);
        private void OnValidate() => HidePreview();
    }
}
