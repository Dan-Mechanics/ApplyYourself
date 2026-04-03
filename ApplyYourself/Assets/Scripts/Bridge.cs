using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Bridge : MonoBehaviour
    {
        private TerrainEvaluator terrainEvaluator;
        private Fade fadeOut;
        private Ending ending;

        public void Setup(TerrainEvaluator terrainEvaluator, Fade fadeOut)
        {
            this.terrainEvaluator = terrainEvaluator;
            this.fadeOut = fadeOut;
            fadeOut.OnFadeComplete += SwitchScenes;
        }

        /// <summary>
        /// Invoked by Timer.
        /// </summary>
        public void GoNextPhase()
        {
            ending = terrainEvaluator.GetEnding();
            fadeOut.BeginFade(false);
            print(ending);
        }

        private void SwitchScenes()
        {
            fadeOut.OnFadeComplete -= SwitchScenes;
            SceneManager.LoadScene(ending.ToString());
        }
    }
}