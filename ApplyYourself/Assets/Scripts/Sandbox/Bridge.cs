using UnityEngine;

namespace ApplyYourself
{
    public class Bridge : MonoBehaviour
    {
        [HideInInspector] public Ending ending;
        private Algorithm algorithm;
        private Portal portal;

        public void Setup(Algorithm algorithm, Portal portal)
        {
            this.algorithm = algorithm;
            this.portal = portal;
        }

        public void GoNextPhase()
        {
            DontDestroyOnLoad(gameObject);
            ending = algorithm.GetEnding();
            print(ending);
            portal.SetScene(ending.ToString());
            portal.Interact();
        }
    }
}