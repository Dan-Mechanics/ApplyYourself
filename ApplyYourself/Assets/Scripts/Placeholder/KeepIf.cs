using UnityEngine;

namespace ApplyYourself
{
    public class KeepIf : BasePlaceholder
    {
        [SerializeField] private Ending ending = default;

        public override void SetAs(Ending ending)
        {
            ending = Utils.Filter(ending, endingOverrides);
            gameObject.SetActive(this.ending == ending);
            if (!gameObject.activeSelf)
                Destroy(gameObject);
        }
    }
}
