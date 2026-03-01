using UnityEngine;

namespace ApplyYourself
{
    public class Algorithm : MonoBehaviour
    {
        [HideInInspector] public Ending ending;
        
        [SerializeField, Range(0f, 1f)] private float dryThreshold = default;
        [SerializeField] private float natureThreshold = default;

        private void Awake() => DontDestroyOnLoad(gameObject);

        public void CompleteSandboxPhase()
        {
            /*int natureCount = GameObject.FindGameObjectsWithTag(natureTag).Length;

            float[] heightmap = terrainManager.GetHeights();
            float highest = heightmap[0];
            float lowest = heightmap[0];

            for (int i = 1; i < heightmap.Length; i++)
            {
                if (heightmap[i] > highest)
                    highest = heightmap[i];

                if (heightmap[i] < lowest)
                    lowest = heightmap[i];
            }

            float avHeight = (highest + lowest) * 0.5f;

            ending = Ending.UnderwaterCity;
            if (natureCount >= natureRequirement)
            {
                ending = Ending.FloatingCity;
            }
            else if (avHeight >= underwaterHeight)
            {
                ending = Ending.FloatingCity;
            }

            print($"OUTCOME: {ending}. avHeight {avHeight}, nature {natureCount}.");
            SceneManager.LoadScene(nextSceneName);*/

            ending = GetEnding(GetComponent<Evaluator>());
            print($"OUTCOME: {ending}.");

            GetComponent<Portal>().Interact();
        }

        private Ending GetEnding(Evaluator evaluator)
        {
            Ending result = Ending.Placeholder;

            evaluator.Evaluate(out int dryUnits, out int wetUnits, out int structureUnits, out int natureUnits);


            return result;
        }
    }
}
