using UnityEngine;

namespace ApplyYourself
{
    public class Bridge : MonoBehaviour
    {
        private TerrainEvaluator terrainEvaluator;
        private Fade fadeOut;
        private Portal portal;
        private Ending ending;

        public void Setup(TerrainEvaluator terrainEvaluator, Portal portal, Fade fadeOut)
        {
            this.terrainEvaluator = terrainEvaluator;
            this.portal = portal;
            this.fadeOut = fadeOut;
            fadeOut.OnYield += SwitchScenes;
        }

        public void GoNextPhase()
        {
            ending = terrainEvaluator.GetEnding();
            portal.SetScene(ending.ToString());
            fadeOut.BeginFade(false);
        }

        private void SwitchScenes(StateBehaviour state)
        {
            print($"{state.name} --> {ending}");
            fadeOut.OnYield -= SwitchScenes;
            portal.Interact();
        }
    }
}