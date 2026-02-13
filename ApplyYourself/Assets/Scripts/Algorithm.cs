using UnityEngine;

namespace ApplyYourself
{
    public class Algorithm : MonoBehaviour
    {
        [SerializeField] private string seedTag = default;

        private Ending ending;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PhaseOneComplete()
        {
            // go to next scene. use some scene managment lore.
            // cook ending.
        }

        private void OnEnable()
        {
            // ??? when swap scenen
            if (GameObject.FindWithTag(seedTag) == null)
                return;


        }
    }
}
