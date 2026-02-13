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
        [SerializeField] private TerrainManager terrainManager = default;
        [SerializeField] private Transform preview = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float range = default;

        [Header("brush !!")]
        [SerializeField] private float area = default;
        [SerializeField] private float speed = default;

        private void Start()
        {
            //preview.gameObject.SetActive(false);
            preview.localScale = 2f * area * Vector3.one;
        }

        private void FixedUpdate()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, range, mask, QueryTriggerInteraction.Ignore);
            if (hasHit)
                preview.position = hit.point;

            // STATE + INPUT !!
            if (!Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                return;

            List<Chunk> neighbourChunks = terrainManager.GetChunksInProximity(hit.point);
            foreach (Chunk chunk in neighbourChunks)
            {
                bool hasChanged = false;
                for (int i = 0; i < chunk.verticies.Length; i++)
                {
                    Vector3 vert = chunk.verticies[i];
                    if (Vector3.Distance(Utils.Flatten(vert), Utils.Flatten(preview.position)) > area)
                        continue;

                    vert.y += speed * Time.fixedDeltaTime;
                    chunk.verticies[i] = vert;
                    hasChanged = true;
                }

                if (hasChanged)
                    chunk.ReloadMesh();
            }
        }
    }
}