using UnityEngine;

namespace ApplyYourself
{
    public class Terraformer : MonoBehaviour 
    {
        [SerializeField] private Camera cam = default;
        [SerializeField] private Transform preview = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float range = default;
        [SerializeField] private BaseBrush[] brushes = default;
        private BaseBrush brush;

        private void Start() => Setup();

        private void Setup()
        {
            for (int i = 0; i < brushes.Length; i++)
            {
                brushes[i].Setup();
            }

            SelectBrush(0);
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

            Collider[] colliders = Physics.OverlapSphere(hit.point, brush.size, mask, QueryTriggerInteraction.Ignore);
            brush.Apply(colliders);
        }

        public void SelectBrush(int index)
        {
            index = Mathf.Clamp(index, 0, brushes.Length - 1);
            brush = brushes[index];
            preview.localScale = 2f * brush.size * Vector3.one;
            //preview.localScale = brush.size * Vector3.one;
            preview.GetComponent<Renderer>().material = brush.previewMaterial;
        }
    }
}