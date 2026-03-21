using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ApplyYourself
{
    public class Interactor : StateBehaviour
    {
        public event Action<string> OnFeedback;

        [SerializeField] private EasyBinding interact = default;
        [SerializeField] private LayerMask mask = default;
        [SerializeField] private float radius = default;
        [SerializeField] private float interactCooldown = default;

        private readonly List<IInteractable> interactables = new List<IInteractable>();
        private float nextInteract;

        public void Setup() => Clear();

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, mask, QueryTriggerInteraction.Ignore);

            interactables.Clear();
            for (int i = 0; i < colliders.Length; i++)
            {
                IInteractable interactable = colliders[i].transform.root.GetComponent<IInteractable>();
                if (interactable != null)
                    interactables.Add(interactable);
            }

            IInteractable closest = interactables.OrderBy(x => Vector3.Distance(x.GetPosition(), transform.position)).FirstOrDefault();
            if (closest == null)
            {
                Clear();
                return;
            }

            OnFeedback?.Invoke($"Press {interact.keyCode}: '{closest.GetHighlight()}'");
            if (!interact.IsHeld || Time.time < nextInteract)
                return;

            closest.Interact();
            nextInteract = Time.time + interactCooldown;
            Clear();
        }

        private void Clear() => OnFeedback?.Invoke(string.Empty);
    }
}
