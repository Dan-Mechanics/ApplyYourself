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
        [SerializeField] private string structureTag = default;
        [SerializeField] private float determineHigh = default;
        [SerializeField] private float determineLow = default;
        [SerializeField] private int determineNature = default;
        [SerializeField] private int determineStructure = default;

        /// <summary>
        /// TODO: Make functional texture for this.
        /// </summary>
        [SerializeField] private PositionToEnding[] conversions = default;

        private void Awake() => DontDestroyOnLoad(gameObject);

        public void CompleteSandboxPhase()
        {
            int structureCount = GameObject.FindGameObjectsWithTag(structureTag).Length;
            int natureCount = GameObject.FindGameObjectsWithTag(natureTag).Length;

            int structureBalance = structureCount - natureCount;
            int x = 0;
            if (structureBalance <= determineNature)
                x = -1;
            else if (structureBalance >= determineStructure)
                x = 1;

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
            int y = 0;
            if (avHeight <= determineLow)
                y = -1;
            else if (avHeight >= determineHigh)
                y = 1;

            Vector2Int pos = new Vector2Int(x, y);
            ending = Ending.Underwater;
            for (int i = 0; i < conversions.Length; i++)
            {
                if (conversions[i].position != pos)
                    continue;

                ending = conversions[i].ending;
                break;
            }

            print($"OUTCOME: {ending}. avHeight {avHeight}, structureBalance {structureBalance}, pos {pos}.");
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
