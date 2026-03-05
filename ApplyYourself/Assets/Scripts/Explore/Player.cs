using System.Linq;
using UnityEngine;

namespace ApplyYourself
{
    public class Player : StateBehaviour
    {
        [SerializeField] private Interactor interactor = default;
        [SerializeField] private PlayerMovement playerMovement = default;
        [SerializeField] private ThirdPersonLook thirdPersonLook = default;
        [SerializeField] private PlayerGraphicRotator graphicRotator = default;
        [SerializeField] private string interactFeedbackName = default; 

        public override void Setup()
        {
            base.Setup();
            interactor.OnFeedback += FindObjectsByType<EasyText>(FindObjectsSortMode.None).
                ToList().Where(x => x.name == interactFeedbackName).First().Write;

            graphicRotator.Setup();
            interactor.Setup();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            interactor.OnFixedUpdate();
            playerMovement.OnFixedUpdate();
            thirdPersonLook.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            interactor.OnUpdate();
            playerMovement.OnUpdate();
            thirdPersonLook.OnUpdate();

            graphicRotator.OnUpdate();
        }
    }
}
