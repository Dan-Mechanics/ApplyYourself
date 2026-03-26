using UnityEngine;

namespace ApplyYourself
{
    public class Bridge : MonoBehaviour
    {
        [HideInInspector] public Ending ending;
        private Algorithm algorithm;
        private Fade fadeOut;
        private Portal portal;

        public void Setup(Algorithm algorithm, Portal portal, Fade fadeOut)
        {
            this.algorithm = algorithm;
            this.portal = portal;
            this.fadeOut = fadeOut;
            fadeOut.OnYield += SwitchScenes;
        }

        private void SwitchScenes(StateBehaviour state)
        {
            fadeOut.OnYield -= SwitchScenes;
            print(ending);
            print(state.name);
            portal.Interact();
        }

        public void GoNextPhase()
        {
            DontDestroyOnLoad(gameObject);
            ending = algorithm.GetEnding();
            portal.SetScene(ending.ToString());
            fadeOut.BeginFade(false);
        }
    }
}