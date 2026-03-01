using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class Evaluator : MonoBehaviour
    {
        [SerializeField] private UnitManager unitManager = default;
        [SerializeField] private WaterManager waterManager = default;
        [SerializeField] private UnitType city = default;
        [SerializeField] private int width = default;
        [SerializeField] private List<UnitType> structureTypes = default;
        [SerializeField] private List<UnitType> natureTypes = default;

        public void Evaluate(out int wetCityUnits, out int structureUnits, out int natureUnits)
        {
            wetCityUnits = structureUnits = natureUnits = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    UnitType type = unitManager.GetTypeAt(x, y);
                    if (unitManager.GetHeightAt(x, y) < waterManager.GetHeightAt(x, y))
                    {
                        if (type == city)
                            wetCityUnits++;

                        continue;
                    }

                    if (structureTypes.Contains(type))
                        structureUnits++;

                    if (natureTypes.Contains(type))
                        natureUnits++;
                }
            }
        }
    }
}
