using UnityEngine;

namespace ApplyYourself
{
    public class Bridge : MonoBehaviour
    {
        [HideInInspector] public Ending ending;

        [SerializeField] private Algorithm algorithm = default;
        [SerializeField] private Portal portal = default;

        public void GoNextPhase()
        {
            DontDestroyOnLoad(gameObject);

            ending = algorithm.GetEnding();
            algorithm = null;

            print(ending);
            portal.Interact();
        }
    }
}
