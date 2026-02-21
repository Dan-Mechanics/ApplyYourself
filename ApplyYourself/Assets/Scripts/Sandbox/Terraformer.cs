using UnityEngine;

namespace ApplyYourself
{
    public class Terraformer : StateBehaviour 
    {
        [SerializeField] private Camera cam = default;
        [SerializeField] private EasyBinding primaryFire = default;
        [SerializeField] private EasyBinding rotate = default;
        [SerializeField] private EasyBinding move = default;
        [SerializeField] private Transform preview = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float range = default;
        [SerializeField] private BaseBrush[] brushes = default;
        private BaseBrush brush;

        public override void Setup()
        {
            base.Setup();
            for (int i = 0; i < brushes.Length; i++)
            {
                brushes[i].Setup();
            }

            SelectBrush(0);
        }

        public override void Exit()
        {
            base.Exit();
            preview.gameObject.SetActive(false);
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
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, range, mask, QueryTriggerInteraction.Ignore);

            preview.gameObject.SetActive(hasHit);
            if (!hasHit)
                return;

            preview.position = hit.point;
            if (!primaryFire.IsHeld)
                return;

            Collider[] colliders = Physics.OverlapSphere(hit.point, brush.size, mask, QueryTriggerInteraction.Ignore);
            brush.Apply(colliders);
        }

        /// <summary>
        /// Called by button.
        /// </summary>
        public void SelectBrush(int index)
        {
            index = Mathf.Clamp(index, 0, brushes.Length - 1);
            brush = brushes[index];
            preview.localScale = 2f * brush.size * Vector3.one;
            preview.GetComponent<Renderer>().material = brush.previewMaterial;
        }
    }
}