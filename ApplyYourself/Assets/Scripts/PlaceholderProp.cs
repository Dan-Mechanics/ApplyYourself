using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Possible:
    /// make noita
    /// make terrain editor like level
    /// make terrain editor marhcing cubes
    /// easy peasy type beat.
    /// 
    /// check slide notes for more notes
    /// </summary>
    public class PlaceholderProp : MonoBehaviour, IPlaceholder
    {
        [SerializeField] private GameObject[] prefabs = default;

        private void SetPrefab(GameObject prefab)
        {
            Transform graphic = transform.Find("graphic");
            if (graphic != null)
                DestroyImmediate(graphic.gameObject);

            GameObject go = Instantiate(prefab);
            go.name = "graphic";
            go.transform.SetParent(transform);
            go.transform.SetLocalPositionAndRotation(prefab.transform.position, prefab.transform.rotation);
            go.transform.localScale = prefab.transform.localScale;
        }

        public void SetAs(Ending ending)
        {
            int index = Utils.GetIndexByName(prefabs, ending);
            SetPrefab(prefabs[index]);
        }
    }
}
