using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// todo, add clever overrides.
    /// </summary>
    public abstract class Placeholder : MonoBehaviour
    {
        [SerializeField] protected string resourceName = default;

        public abstract void SetAs(Ending ending);
    }
}
