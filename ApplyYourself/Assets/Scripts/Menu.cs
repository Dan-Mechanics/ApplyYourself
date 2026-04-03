using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class Menu : MonoBehaviour
    {
        private readonly FSM fsm = new FSM();
        private Button playButton;
        private Portal portal;
        private Fade fadeOut;

        private void Awake()
        {
            playButton = FindAnyObjectByType<Button>();
            portal = FindAnyObjectByType<Portal>();
            fadeOut = FindAnyObjectByType<Fade>();
        }

        private void Start()
        {
            //fsm.AddTransition(new StateTransition(dialogueSystem, player));
            //fsm.AddTransition(new StateTransition(fadeIn, player));
            fsm.AddState(fadeOut);
            portal.OnInteract += BeginGoToNextScene;
            fadeOut.OnYield += SwitchScenes;

            fsm.Open(null);
            //fadeIn.BeginFade(true);
        }

        private void SwitchScenes(StateBehaviour state)
        {
            print(state.name);
            portal.Interact();
        }

        private void BeginGoToNextScene()
        {
            playButton.interactable = false;
            fadeOut.BeginFade(false);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
