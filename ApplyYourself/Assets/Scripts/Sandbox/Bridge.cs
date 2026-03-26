using UnityEngine;

namespace ApplyYourself
{
    public class Bridge : MonoBehaviour
    {
        [HideInInspector] public Ending ending;
        private TerrainEvaluator terrainEvaluator;
        private Fade fadeOut;
        private Portal portal;

        public void Setup(TerrainEvaluator terrainEvaluator, Portal portal, Fade fadeOut)
        {
            this.terrainEvaluator = terrainEvaluator;
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
            ending = terrainEvaluator.GetEnding();
            portal.SetScene(ending.ToString());
            fadeOut.BeginFade(false);
        }
    }
}