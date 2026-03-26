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
        private QuestHandler questHandler;
        
        public void Setup(QuestHandler questHandler, EasyText easyText, ILookInput lookInput, IMoveInput moveInput)
        {
            this.questHandler = questHandler;
            interactor.OnFeedback += easyText.Write;

            playerMovement.Assign(moveInput);
            graphicRotator.Assign(moveInput);
            thirdPersonLook.Assign(lookInput);
            thirdPersonLook.OnUpdate();

            interactor.Setup();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Exit()
        {
            base.Exit();
            questHandler.Exit();
        }
        
        public override void Enter()
        {
            base.Enter();
            questHandler.Enter();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            interactor.OnFixedUpdate();
            playerMovement.OnFixedUpdate();
            thirdPersonLook.OnFixedUpdate();
            questHandler.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            interactor.OnUpdate();
            playerMovement.OnUpdate();
            thirdPersonLook.OnUpdate();
            graphicRotator.OnUpdate();
            questHandler.OnFixedUpdate();
        }
    }
}
