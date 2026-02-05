using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public abstract class Placeholder : MonoBehaviour
    {
        [SerializeField] protected string resourceName = default;
        [SerializeField] protected EndingOverride[] endingOverrides = default;

        public abstract void SetAs(Ending ending);
    }
}
