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
            fsm.AddState(fadeOut);
            portal.OnInteract += BeginGoToNextScene;
            fadeOut.OnFadeComplete += portal.SwitchScenes;

            fsm.Open(null);
        }

        private void BeginGoToNextScene()
        {
            playButton.interactable = false;
            portal.OnInteract -= BeginGoToNextScene;
            fadeOut.BeginFade(false);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
