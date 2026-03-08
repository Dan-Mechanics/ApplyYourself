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
        private CanvasGroup canvasGroup;

        public void Assign(CanvasGroup canvasGroup, GoToQuest goToQuest)
        {
            this.canvasGroup = canvasGroup;
        }

        public override void Setup()
        {
            base.Setup();
            interactor.OnFeedback += FindObjectsByType<EasyText>(FindObjectsSortMode.None).
                ToList().Where(x => x.name == interactFeedbackName).First().Write;

            WASD wasd = new WASD();
            playerMovement.Assign(wasd);
            graphicRotator.Assign(wasd);
            thirdPersonLook.Assign(FindAnyObjectByType<SensitivityMouse>());

            interactor.Setup();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Exit()
        {
            base.Exit();
            canvasGroup.alpha = 0f;
        }
        
        public override void Enter()
        {
            base.Enter();
            canvasGroup.alpha = 1f;
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
