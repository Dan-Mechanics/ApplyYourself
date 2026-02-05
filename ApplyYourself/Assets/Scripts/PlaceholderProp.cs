using UnityEngine;

namespace ApplyYourself
{
    public class PlaceholderAmbient : Placeholder
    {
        private void SetGraphic(GameObject prefab)
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

        public override void SetAs(Ending ending)
        {
            ending = Utils.Filter(ending, endingOverrides);
            SetGraphic(Resources.Load<GameObject>($"{ending}/{resourceName}"));
        }
    }
}
