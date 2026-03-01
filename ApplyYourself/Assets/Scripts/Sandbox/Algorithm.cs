using UnityEngine;

namespace ApplyYourself
{
    public class Algorithm : MonoBehaviour
    {
        [HideInInspector] public Ending ending;

        [SerializeField] private Evaluator evaluator = default;
        [SerializeField] private Portal portal = default;
        [SerializeField] private int dryThreshold = default;
        [SerializeField] private int minCombinedUnitsChanged = default;

        public void CompleteSandboxPhase()
        {
            DontDestroyOnLoad(gameObject);

            ending = GetEnding();
            print(ending);
            portal.Interact();
        }

        private Ending GetEnding()
        {
            evaluator.Evaluate(out int wetCityUnits, out int structureUnits, out int natureUnits);
            if (wetCityUnits <= dryThreshold)
            {
                if (structureUnits >= natureUnits)
                    return Ending.IndustrialCity;

                return Ending.NatureCity;
            }

            if (structureUnits + natureUnits > minCombinedUnitsChanged)
                return Ending.FloatingCity;

            return Ending.UnderwaterCity;
        }
    }
}
