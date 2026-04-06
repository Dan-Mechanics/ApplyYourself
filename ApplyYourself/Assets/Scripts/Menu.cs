using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class Menu : MonoBehaviour
    {
        private readonly FSM fsm = new FSM();
        private Button[] buttons;
        private Portal portal;
        private Fade fadeOut;

        private void Awake()
        {
            portal = FindAnyObjectByType<Portal>();
            fadeOut = FindAnyObjectByType<Fade>();
            buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
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
            buttons.ToList().ForEach(x => x.interactable = false);
            portal.OnInteract -= BeginGoToNextScene;
            fadeOut.BeginFade(false);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
