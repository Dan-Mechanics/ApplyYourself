using UnityEngine;

namespace ApplyYourself
{
    public class Player : StateBehaviour
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private Interactor interactor = default;
        [SerializeField] private PlayerMovement playerMovement = default;

        public override void Setup()
        {
            Transform cam = GameObject.FindWithTag("MainCamera").transform;

            LerpFollow lerpFollow = cam.GetComponent<LerpFollow>();
            lerpFollow.SetTarget(target);
            lerpFollow.transform.LookAt(transform);

            interactor.SetCamera(cam.GetComponent<Camera>());
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            interactor.OnFixedUpdate();
            playerMovement.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            interactor.OnUpdate();
            playerMovement.OnUpdate();
        }
    }
}
