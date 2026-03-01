using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class Terraformer : StateBehaviour 
    {
        [SerializeField] private Camera cam = default;
        [SerializeField] private EasyBinding primaryFire = default;
        [SerializeField] private EasyBinding rotate = default;
        [SerializeField] private EasyBinding move = default;
        [SerializeField] private GameObject preview = default;
        [SerializeField] private LayerMask mask = default;

        [SerializeField] private float range = default;
        [SerializeField] private float spacing = default;

        private List<Brush> brushes;
        private Brush brush;

        public void SetBrushes(List<Brush> brushes) => this.brushes = brushes;

        public override void Setup()
        {
            base.Setup();
            SelectBrush(0);
        }

        public override void Exit()
        {
            base.Exit();
            preview.SetActive(false);
            Cursor.visible = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (rotate.IsHeld || move.IsHeld)
                YieldState();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            bool rayHasHit = Physics.Raycast(ray, out RaycastHit hit, range, mask, QueryTriggerInteraction.Ignore);

            if (rayHasHit)
            {
                preview.SetActive(true);
                preview.transform.position = hit.point;
                if (primaryFire.IsHeld)
                {
                    // TODO: NON-ALLOC HERE. !!
                    Collider[] colliders = Physics.OverlapSphere(hit.point, brush.size, mask, QueryTriggerInteraction.Ignore);

                    List<Vector2Int> positions = new List<Vector2Int>();
                  //  Debug.Log(colliders.Length);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Vector3 pos = colliders[i].transform.position;
                        pos -= 0.5f * spacing * Vector3.one;
                        pos /= spacing;
                        positions.Add(new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)));
                    }

                    brush.Apply(positions);
                }
            }
            else
            {
                preview.SetActive(false);
            }
        }

        /// <summary>
        /// Called by button.
        /// </summary>
        public void SelectBrush(int index)
        {
            index = Mathf.Clamp(index, 0, brushes.Count - 1);
            brush = brushes[index];
            preview.transform.localScale = 2f * brush.size * Vector3.one;
            preview.GetComponent<Renderer>().material = brush.previewMaterial;
        }
    }
}