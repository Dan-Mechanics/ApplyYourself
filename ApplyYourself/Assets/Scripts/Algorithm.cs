using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Algorithm : MonoBehaviour
    {
        [HideInInspector] public Ending ending;
        
        [SerializeField] private string seedTag = default;
        [SerializeField] private TerrainManager terrainManager = default;
        [SerializeField] private string natureTag = default;
        [SerializeField] private string structureTag = default;

        [SerializeField] private int structuralTippingPoint = default;
        [SerializeField] private float highTippingPoint = default;

      //  private Ending ending;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PhaseOneComplete()
        {
            int structureCount = GameObject.FindGameObjectsWithTag(structureTag).Length;
            int natureCount = GameObject.FindGameObjectsWithTag(natureTag).Length;

            int decorationBalance = structureCount - natureCount;


            float biggest = 0f;
            float smallest = 0f;
            /*foreach (var chunk in terrainManager.chunks)
            {
                float height = chunk.Value.height;
                if (height > biggest)
                    biggest = height;

                if (height < smallest)
                    smallest = height;
            }*/

            float balanceHeight = biggest + smallest / 2f;
            print($"OUTCOME: balance{decorationBalance} | height{balanceHeight}");

            bool high = balanceHeight > highTippingPoint;
            bool structure = decorationBalance > structuralTippingPoint;

            print($"OUTCOME: {(high ? "high" : "low")} {(structure ? "city heavy" : "nature heavy")}");
            ending = high ? Ending.Dry : Ending.Wet;
            SceneManager.LoadScene("Scene");
        }
    }
}
