using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class PrefabSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabs = default;

        private void Start()
        {
            prefabs.ForEach(x => Instantiate(x, x.transform.position, x.transform.rotation));
            Destroy(gameObject);
        }
    }
}
