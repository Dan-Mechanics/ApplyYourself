using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    ///  INPUT + STATE !!
    /// </summary>
    public class Terraformer : MonoBehaviour 
    {
        [SerializeField] private Camera cam = default;
        [SerializeField] private Transform preview = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float range = default;

        private void Start()
        {
            preview.gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, range, mask, QueryTriggerInteraction.Ignore);

            preview.gameObject.SetActive(hasHit);
            if (!hasHit)
                return;

            preview.position = hit.point;
        }
    }
}