using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class Evaluator : MonoBehaviour
    {
        [SerializeField] private LandManager landManager = default;
        [SerializeField] private WaterManager waterManager = default;
        [SerializeField] private UnitType city = default;
        [SerializeField] private int width = default;
        [SerializeField] private List<UnitType> structureTypes = default;
        [SerializeField] private List<UnitType> natureTypes = default;

        public void Evaluate(out int dryUnits, out int wetUnits, out int structureUnits, out int natureUnits)
        {
            dryUnits = wetUnits = structureUnits = natureUnits = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    UnitType type = landManager.GetTypeAt(x, y);
                    float landHeight = landManager.GetHeightAt(x, y);
                    float waterHeight = waterManager.GetHeightAt(x, y);
                    bool isLand = landHeight >= waterHeight;

                    if (type == city)
                    {
                        if (isLand)
                        {
                            dryUnits++;
                        }
                        else
                        {
                            wetUnits++;
                        }
                    }
                    else if (isLand)
                    {
                        if (structureTypes.Contains(type))
                        {
                            structureUnits++;
                        }
                        else if (natureTypes.Contains(type))
                        {
                            natureUnits++;
                        }
                    }
                }
            }
        }
    }
}
