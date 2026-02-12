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
        [SerializeField] private TerrainGenerator manager = default;
        [SerializeField] private Transform preview = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float range = default;

        [Header("brush !!")]
        [SerializeField] private float area = default;
        [SerializeField] private float speed = default;

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

            if (!Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                return;

            // now this is the part that needs to be in a brush.
            List<Chunk> chunks = manager.GetChunks(hit.point);
            foreach (Chunk chunk in chunks)
            {
                bool hasChanged = false;
                for (int i = 0; i < chunk.verticies.Length; i++)
                {
                    if (Vector3.Distance(chunk.verticies[i], hit.point) > area)
                        continue;

                    chunk.verticies[i].y += speed * Time.fixedDeltaTime;
                    hasChanged = true;
                }

                if (hasChanged)
                    chunk.ReloadMesh();
            }
        }
    }
}