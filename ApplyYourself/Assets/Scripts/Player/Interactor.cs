using UnityEngine;

namespace ApplyYourself
{
    public class Interactor : StateBehaviour
    {
        [SerializeField] private EasyBinding interact = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float range = default;
        private Camera cam;

        public void SetCamera(Camera cam) => this.cam = cam;

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!interact.WasPressed)
                return;
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, range, mask, QueryTriggerInteraction.Ignore))
                return;

            if (!hit.transform.TryGetComponent(out IInteractable interactable))
                return;

            interactable.Interact();
        }
    }
}
