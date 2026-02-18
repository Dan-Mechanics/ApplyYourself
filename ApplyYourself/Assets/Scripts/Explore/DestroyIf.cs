using UnityEngine;

namespace ApplyYourself
{
    public class DestroyIf : BasePlaceholder
    {
        [SerializeField] private Ending ending = default;

        public override void SetAs(Ending ending)
        {
            ending = Utils.Filter(ending, endingOverrides);
            if (this.ending == ending)
                Destroy(gameObject);
        }
    }
}
