using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public abstract class BasePlaceholder : MonoBehaviour
    {
        [SerializeField] protected string resourceName = default;
        [SerializeField] protected EndingOverride[] endingOverrides = default;

        public abstract void SetAs(Ending ending);
    }
}
