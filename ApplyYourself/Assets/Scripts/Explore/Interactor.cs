using UnityEngine;

namespace ApplyYourself
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private EasyBinding interact = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float range = default;
        private Camera cam;

        private void Start() => cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        private void Update()
        {
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
