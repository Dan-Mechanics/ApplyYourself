using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Algorithm : MonoBehaviour
    {
        [HideInInspector] public Ending ending;
        
        [SerializeField] private TerrainManager terrainManager = default;
        [SerializeField] private Object placeholderScene = default;
        [SerializeField] private string natureTag = default;
        [SerializeField] private float underwaterHeight = default;
        [SerializeField] private int natureRequirement = default;

        private void Awake() => DontDestroyOnLoad(gameObject);

        public void CompleteSandboxPhase()
        {
            int natureCount = GameObject.FindGameObjectsWithTag(natureTag).Length;

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
            if (avHeight <= underwaterHeight)
            {
                ending = Ending.UnderwaterCity;
            }
            else if (natureCount >= natureRequirement)
            {
                ending = Ending.FloatingCity;
            }
            else
            {
                ending = Ending.FloatingCity;
            }

            print($"OUTCOME: {ending}. avHeight {avHeight}, nature {natureCount}.");
            SceneManager.LoadScene(placeholderScene.name);
        }

        [System.Serializable]
        private struct PositionToEnding
        {
            public Vector2Int position;
            public Ending ending;
        }
    }
}
